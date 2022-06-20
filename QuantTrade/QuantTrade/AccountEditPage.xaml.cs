using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantTrade.Model;
using QuantTrade.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuantTrade
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountEditPage : ContentPage
    {
        public AccountEditPage(BaseAccount editAccount)
        {
            InitializeComponent();
            
            (Resources["vm"] as AccountEditVM).EditAccount = editAccount;
            
            AccountDescription.Text = editAccount.Description;
        }
    }
}