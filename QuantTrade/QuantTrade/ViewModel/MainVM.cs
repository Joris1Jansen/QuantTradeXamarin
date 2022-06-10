using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using QuantTrade.Helpers;
using QuantTrade.Helpers.Messages;
using QuantTrade.Utils;
using Xamarin.Forms;

namespace QuantTrade.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public Command LoginCommand { get; set; }

        public RegisterUserCommand RegisterUserCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string email;
        public string Email 
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("FormIsValid");
                EmailError = FormValidationUtil.ValidateEmail(value);
            }
        }
        
        private string emailError;
        public string EmailError
        {
            get
            {
                return emailError;
            }
            set
            {
                emailError = value;
                OnPropertyChanged("FormIsValid");
                OnPropertyChanged("EmailError");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("FormIsValid");
                PasswordError = FormValidationUtil.ValidatePassword(value);
            }
        }

        private string passwordError;
        public string PasswordError
        {
            get
            {
                return passwordError;
            }
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
                OnPropertyChanged("FormIsValid");
            }
        }
        
        private bool formIsValid;
        public bool FormIsValid
        {
            get
            {
                return FormValidationUtil.EntriesHaveTekst(Email, Password) && FormValidationUtil.EntriesHaveNoError(EmailError, PasswordError);
            }
        }

        public MainVM()
        {
            LoginCommand = new Command<object>(Login, CanLogin);
            RegisterUserCommand = new RegisterUserCommand(this);
        }

        private async void Login(object parameter)
        {
            bool result = await Auth.LoginUser(Email, Password);
            if (result)
            {
                //Start task for updating current holdings
                var holdingsMessage = new StartUpdatingHoldingsTask();
                MessagingCenter.Send(holdingsMessage, "StartUpdatingHoldingsTask");

                var accountsMessage = new StartUpdatingAccountsTask();
                MessagingCenter.Send(accountsMessage, "StartUpdatingAccountsTask");
                
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
        }
        
        
        public async Task InitializeAsync()
        {
            await Task.Run(() =>
            {
                LoginCommand = new Command(Login, CanLogin);
            });
        }
        

        private bool CanLogin(object parameter)
        {
            return FormIsValid;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RegisterUserNavigation()
        {
            App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }

    public class RegisterUserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainVM _vm;

        public RegisterUserCommand(MainVM vm)
        {
            _vm = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _vm.RegisterUserNavigation();
        }
    }
}