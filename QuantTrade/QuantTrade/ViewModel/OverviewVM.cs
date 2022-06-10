using System;
using System.Collections.ObjectModel;
using Coinbase.Models;
using QuantTrade.Firestore;
using QuantTrade.Helpers.Messages;
using QuantTrade.Model;
using Xamarin.Forms;

namespace QuantTrade.ViewModel
{
    public class OverviewVM
    {
        public ObservableCollection<CBAccount> CBAccounts { get; set; }

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
            CBAccounts = new ObservableCollection<CBAccount>();
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
    }
}