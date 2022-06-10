using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Coinbase;
using QuantTrade.CustomView;
using QuantTrade.Helpers;
using QuantTrade.Model;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;

namespace QuantTrade.ViewModel
{
    public class AddCoinbaseVM : INotifyPropertyChanged
    {
        public Command AddBrokerCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private string description;
        public string Description 
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("FormIsValid");
            }
        }

        private string publicKey;
        public string PublicKey 
        {
            get { return publicKey; }
            set
            {
                publicKey = value;
                OnPropertyChanged("FormIsValid");
            }
        }
        

        private string privateKey;
        public string PrivateKey
        {
            get { return privateKey; }
            set
            {
                privateKey = value;
                OnPropertyChanged("FormIsValid");
            }
        }
        
        
        private bool formIsValid;
        public bool FormIsValid
        {
            get
            {
                return !string.IsNullOrEmpty(PrivateKey) && !string.IsNullOrEmpty(PublicKey) && !string.IsNullOrEmpty(Description);
            }
        }

        public AddCoinbaseVM()
        {
            AddBrokerCommand = new Command<bool>(AddBroker, CanAddBroker);
        }

        private async void AddBroker(bool parameter)
        {
            await PopupNavigation.Instance.PushAsync(new LoadingSpinner());
            var client = new CoinbaseClient(new ApiKeyConfig{ ApiKey = PublicKey, ApiSecret = PrivateKey});
            try
            {
                var currUser = await client.Users.GetCurrentUserAsync();
                await client.Accounts.ListAccountsAsync(new PaginationOptions {Limit = Constants.PAGINATION_NUM});

                CBAccount cbAcccount = new CBAccount(currUser.Data.Name, Description, PublicKey, PrivateKey, DateTime.Now, BrokersEnum.Coinbase);
                bool result = await cbAcccount.RetrieveAccounts();
                if (result)
                {
                    await PopupNavigation.Instance.PopAsync();
                    App.Current.MainPage.Navigation.PopAsync();
                }

            }
            catch
            {
                await PopupNavigation.Instance.PopAsync();
                App.Current.MainPage.DisplayAlert("Failure", "Unable to connect with these API keys", "Ok");
            }
        }
        
        private bool CanAddBroker(bool parameter)
        {
            return FormIsValid;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}