using System;
using System.Linq;
using QuantTrade.Helpers.Messages;
using QuantTrade.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuantTrade
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewPage : ContentPage
    {
        private OverviewVM vm;
        
        private float[] coordinates = {0, 1, 0}; 
        public OverviewPage()
        {
            InitializeComponent();

            vm = Resources["vm"] as OverviewVM;
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            vm.GetAccountOverview();
            
            Accelerometer.ReadingChanged += UpdateAccount;
            Accelerometer.Start(SensorSpeed.UI);
        }
        
        protected override void OnDisappearing()
        {
            Accelerometer.ReadingChanged -= UpdateAccount;
            Accelerometer.Stop();
            
        }
        
        public void UpdateAccount(object sender, AccelerometerChangedEventArgs args)
        {
            var coorArray = new[]
                {args.Reading.Acceleration.X, args.Reading.Acceleration.Y, args.Reading.Acceleration.Z};

            if (!coordinates.SequenceEqual(coorArray))
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    foreach (var account in vm.CBAccounts)
                    {
                        account.UpdateHoldings();
                    }
                    
                    var message = new HoldingsUpdatedMessage();
                    Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send<HoldingsUpdatedMessage>(message, "HoldingsUpdatedMessage");
                    });
                    
                    coordinates = coorArray;
                });
            }
        }
    }
}