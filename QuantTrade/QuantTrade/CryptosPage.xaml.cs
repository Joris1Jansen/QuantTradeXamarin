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
    public partial class CryptosPage : ContentPage
    {
        private CryptosVM vm;
        public CryptosPage()
        {
            InitializeComponent();
            
            vm = Resources["vm"] as CryptosVM;
            BindingContext = vm;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            vm.GetTopTen();
        }
    }
}