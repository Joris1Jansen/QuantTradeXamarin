using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using QuantTrade.Utils;
using QuantTrade.Helpers;
using Xamarin.Forms;

namespace QuantTrade.ViewModel
{
    public class RegisterVM : INotifyPropertyChanged
    {
        public LoginUserCommand LoginUserCommand { get; set; }
        public Command RegisterCommand { get; set; }
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
        
        private string verifyPassword;
        public string VerifyPassword
        {
            get { return verifyPassword; }
            set
            {
                verifyPassword = value;
                OnPropertyChanged("FormIsValid");
                VerifyPasswordError = FormValidationUtil.ValidateVerificationPassword(Password, value);
            }
        }
        
        private string verifyPasswordError;
        public string VerifyPasswordError
        {
            get
            {
                return verifyPasswordError;
            }
            set
            {
                verifyPasswordError = value;
                OnPropertyChanged("VerifyPasswordError");
                OnPropertyChanged("FormIsValid");
            }
        }

        private bool formIsValid;
        public bool FormIsValid
        {
            get
            {
                return FormValidationUtil.EntriesHaveTekst(Email, Password, VerifyPassword) && FormValidationUtil.EntriesHaveNoError(EmailError, PasswordError, VerifyPasswordError);
            }
        }
        
        public RegisterVM()
        {
            RegisterCommand = new Command<object>(Register, CanRegister);
            LoginUserCommand = new LoginUserCommand(this);
        }
        
        private async void Register(object parameter)
        {
            bool result = await Auth.RegisterUser(Email, Password);
            if (result)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Success", "Ok");
                App.Current.MainPage.Navigation.PopAsync();
            }
        }
        
        public async Task InitializeAsync()
        {
            await Task.Run(() =>
            {
                RegisterCommand = new Command(Register, CanRegister);
            });
        }
        
        private bool CanRegister(object parameter)
        {
            return FormIsValid;
        }

        public void LoginUserNavigation()
        { 
            App.Current.MainPage.Navigation.PopAsync();
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    public class LoginUserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private RegisterVM _vm;

        public LoginUserCommand(RegisterVM vm)
        {
            _vm = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _vm.LoginUserNavigation();
        }
    }
}