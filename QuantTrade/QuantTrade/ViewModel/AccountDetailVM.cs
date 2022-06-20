using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuantTrade.Model;
using Xamarin.Forms;

namespace QuantTrade.ViewModel
{
    public class AccountDetailVM : INotifyPropertyChanged
    {
        private BaseAccount account;
        public BaseAccount Account
        {
            get { return account; }
            set
            {
                account = value;
                OnPropertyChanged("Account");
            }
        }
        
        public Command EditAccountCommand { get; set; }

        public AccountDetailVM()
        {
            EditAccountCommand = new Command<BaseAccount>(EditBroker);
        }
        
        private async void EditBroker(BaseAccount parameter)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AccountEditPage(parameter));
        }
        
        private bool CanEditBroker(bool parameter)
        {
            return true;
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateAccount()
        {
            Account.UpdateHoldings();
        }
        
        public void SetAccount(BaseAccount account)
        {
            Account = account;
        }
    }
}