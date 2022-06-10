using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantTrade.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuantTrade
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBrokerPage : ContentPage
    {
        private AddBrokerVM vm;
        public AddBrokerPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as AddBrokerVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            vm.GetBrokers();
        }
    }
}