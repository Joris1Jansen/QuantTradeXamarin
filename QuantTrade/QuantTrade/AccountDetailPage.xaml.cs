using System;
using System.Linq;
using QuantTrade.Model;
using QuantTrade.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuantTrade
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountDetailPage : ContentPage
    {
        private AccountDetailVM vm;
        private BaseAccount account;
        private float[] coordinates = {0, 1, 0}; 
        public AccountDetailPage(BaseAccount selectedAccount)
        {
            InitializeComponent();
            
            vm = Resources["vm"] as AccountDetailVM;
            account = selectedAccount;
            // Accelerometer.ReadingChanged += UpdateAccount;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
        
            vm.SetAccount(account);

            Accelerometer.ReadingChanged += UpdateAccount;
            
            // Does not work on an emulator
            // Accelerometer.ShakeDetected += UpdateAccount;
            Accelerometer.Start(SensorSpeed.Game);
        }
        
        protected override void OnDisappearing()
        {
            Accelerometer.ReadingChanged -= UpdateAccount;
            
            // Does not work on an emulator
            // Accelerometer.ShakeDetected -= UpdateAccount;
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
                    // coordinates = coorArray;
                    // account.UpdateHoldings();
                    // vm.SetAccount(account);
                });
            }
        }
    }
}
