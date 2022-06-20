using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coinbase;
using QuantTrade.Firestore;
using QuantTrade.Model.Interface;
using SQLite;

namespace QuantTrade.Model
{
    public class BaseAccount : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        
        public BrokersEnum Broker { get; set; }
        
        public string Username { get; set; }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged("Description");
            }
        }
        
        public string UserId { get; set; }
        
        public string PublicKey { get; set; }
        
        public string PrivateKey { get; set; }
        
        private ObservableCollection<BaseHolding> _holdings;
        public ObservableCollection<BaseHolding> Holdings
        {
            get { return _holdings; }
            set
            {
                _holdings = value;
                RaisePropertyChanged();
            }
        }
        
        // public ObservableCollection<BaseHolding> Holdings { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        public DateTime CreatedAt { get; set;  }
        
        private CoinbaseClient Client;
        
        
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
        
        public int AmountOfHoldings
        {
            get
            {
                return Holdings.Count;
            }
        }

        public decimal CurrentValue
        {
            get
            {
                var currVal = (decimal)0;
                foreach (var holding in Holdings)
                {
                    currVal += holding.Worth;
                }

                return currVal;
            }
        }
        
        public BaseAccount(string username, string description, string publicKey, string privateKey, DateTime createdAt, BrokersEnum broker)
        {
            Username = username;
            Description = description;
            PublicKey = publicKey;
            PrivateKey = privateKey;
            Holdings = new ObservableCollection<BaseHolding>();
            UpdatedAt = DateTime.Now;
            CreatedAt = createdAt;
            Broker = broker;
        }
        
        public async void UpdateHoldings()
        {
            Client = new CoinbaseClient(new ApiKeyConfig{ ApiKey = PublicKey, ApiSecret = PrivateKey});
            
            foreach (CBHolding holding in Holdings)
            {
                var newCurrentPrice = await Client.Data.GetExchangeRatesAsync(holding.Currency);
                var newAmount = Client.Accounts.GetAccountAsync(holding.WalletId);

                holding.CurrentPrice = newCurrentPrice.Data.Rates["USD"];
                holding.Amount = newAmount.Result.Data.Balance.Amount;
            }
            
            UpdatedAt = DateTime.Now;
            
            await FireCBAccount.Update(this as CBAccount);
        }
    }
}