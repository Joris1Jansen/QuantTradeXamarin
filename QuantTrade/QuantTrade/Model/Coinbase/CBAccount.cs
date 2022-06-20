using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coinbase;
using Coinbase.Models;
using QuantTrade.Firestore;
using QuantTrade.Helpers;
using QuantTrade.Model.Interface;

namespace QuantTrade.Model
{
    public class CBAccount : BaseAccount, IBaseAccount, INotifyPropertyChanged
    {
        private CoinbaseClient Client;
        public CBAccount(
            string username, 
            string description, 
            string publicKey, 
            string privateKey, 
            DateTime createdAt, 
            BrokersEnum broker) : base(username, description, publicKey, privateKey, createdAt, broker)
        {
            
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
            
            await FireCBAccount.Update(this);
        }
        
        public async Task<bool> RetrieveAccounts()
        {
            //Clear all current holdings
            Holdings.Clear();
            
            //Create client for API calls
            Client = new CoinbaseClient(new ApiKeyConfig{ ApiKey = PublicKey, ApiSecret = PrivateKey});
            
            var page = await Client.Accounts.ListAccountsAsync(new PaginationOptions{Limit = Constants.PAGINATION_NUM});
            await LoopTroughPages(page);
            
            UpdatedAt = DateTime.Now;
            bool result = await SaveAccount(this);
            return result;
        }

        public async Task<bool> UpdateAccounts()
        {
            //Clear all current holdings
            Holdings.Clear();
            
            //Create client for API calls
            Client = new CoinbaseClient(new ApiKeyConfig{ ApiKey = PublicKey, ApiSecret = PrivateKey});
            
            var page = await Client.Accounts.ListAccountsAsync(new PaginationOptions{Limit = Constants.PAGINATION_NUM});
            await LoopTroughPages(page);
            
            UpdatedAt = DateTime.Now;
            var result = await FireCBAccount.Update(this);
            return result;
        }
        
        public async Task LoopTroughPages(PagedResponse<Account> page)
        {
            await AddToHoldings(page);
            if (page.Data.Length == Constants.PAGINATION_NUM)
            {
                var newPage = await GetNewPage(page);
                await LoopTroughPages(newPage);
            }
        }
        
        public async Task AddToHoldings(PagedResponse<Account> page)
        {
            foreach (var holding in page.Data)
            {
                if (holding.Balance.Amount > 0)
                {
                    var price = await Client.Data.GetExchangeRatesAsync(holding.Balance.Currency);
                    Holdings.Add(new CBHolding(holding.Id, holding.Balance.Amount, holding.Balance.Currency, price.Data.Rates["USD"]));
                    
                }
            }
        }
        
        public async Task<PagedResponse<Account>> GetNewPage(PagedResponse<Account> page)
        {
            return await Client.GetNextPageAsync(page);
        }

        private async Task<bool> SaveAccount(CBAccount cbAccount)
        {
            bool result = FireCBAccount.Insert(cbAccount);
            if (result)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Broker successfully inserted", "ok");
                return result;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Failure", "Broker failed to be inserted", "ok");
                return result;
            }
        }
        
        private async Task<bool> UpdateAccounts(CBAccount cbAccount)
        {
            bool result = await FireCBAccount.Update(cbAccount);
            return result;
        }
        
    }
}