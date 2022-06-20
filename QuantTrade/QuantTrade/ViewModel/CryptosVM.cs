using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Coinbase;
using QuantTrade.Helpers;
using QuantTrade.Model;

namespace QuantTrade.ViewModel
{
    public class CryptosVM : INotifyPropertyChanged
    {
        
        private ObservableCollection<BaseHolding> topTen;

        public ObservableCollection<BaseHolding> TopTen
        {
            get { return topTen; }
            set
            {
                topTen = value;
                foreach (var acc in TopTen)
                {
                    OnPropertyChanged();
                }
            }
        }
        
        public CryptosVM()
        {
            TopTen = new ObservableCollection<BaseHolding>();
        }
        
        public async void GetTopTen()
        {
            TopTen.Clear();
            var coins = new List<string>{"BTC", "ETH", "BNB", "ADA", "XRP", "SOL", "DOGE", "DOT", "DAI", "TRX"};
            foreach (var coin in coins)
            {
                var holding = await CoinPrice(coin);
                TopTen.Add(holding);
            }
        }
        
        public async Task<BaseHolding> CoinPrice(string coin)
        {
            var client = new CoinbaseClient(new ApiKeyConfig{ ApiKey = Constants.COINBASE_KEY, ApiSecret = Constants.COINBASE_SECRET});
            
            var currentPrice = await client.Data.GetExchangeRatesAsync(coin);

            var currPrice = currentPrice.Data.Rates["USD"];
            var baseCoin = new BaseHolding(0, coin, currPrice);
            return baseCoin;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}