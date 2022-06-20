using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Coinbase.Models;
using QuantTrade.Firestore;
using QuantTrade.Helpers.Messages;
using QuantTrade.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuantTrade.ViewModel
{
    public class OverviewVM : INotifyPropertyChanged
    {
        private ObservableCollection<BaseAccount> cBAccounts;

        public ObservableCollection<BaseAccount> CBAccounts
        {
            get { return cBAccounts; }
            set
            {
                cBAccounts = value;
                foreach (var acc in CBAccounts)
                {
                    OnPropertyChanged();
                }
            }
        }

        private BaseAccount selectedAccount;

        public BaseAccount SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                selectedAccount = value;
                if (selectedAccount != null)
                {
                    App.Current.MainPage.Navigation.PushAsync(new AccountDetailPage(selectedAccount));
                }
            }
        }

        public OverviewVM()
        {
            CBAccounts = new ObservableCollection<BaseAccount>();
            HandleReceiveMessages();
        }

        void HandleReceiveMessages()
        {
            MessagingCenter.Subscribe<HoldingsUpdatedMessage> (this, "HoldingsUpdatedMessage", message => {
                Device.BeginInvokeOnMainThread(GetAccountOverview);
            });
            
            // MessagingCenter.Subscribe<AccountsUpdatedMessage> (this, "AccountsUpdatedMessage", message => {
            //     Device.BeginInvokeOnMainThread(GetAccountOverview);
            // });
        }
        
        public async void GetAccountOverview()
        {
            CBAccounts.Clear();
            var cbAccounts = await FireCBAccount.Read();
            foreach (var account in cbAccounts)
            {
                CBAccounts.Add(account);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}