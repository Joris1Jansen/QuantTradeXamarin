using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuantTrade.Model;

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
        
        public AccountDetailVM()
        {
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