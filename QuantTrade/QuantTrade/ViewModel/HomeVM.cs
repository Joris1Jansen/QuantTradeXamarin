using System;
using System.Windows.Input;

namespace QuantTrade.ViewModel
{
    public class HomeVM
    {
        public NewBrokerCommand NewBrokerCommand { get; set; }

        public HomeVM()
        {
            NewBrokerCommand = new NewBrokerCommand(this);
        }

        public async void NewBrokerNavigation()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddBrokerPage());
        }
    }

    public class NewBrokerCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private HomeVM _vm;

        public NewBrokerCommand(HomeVM vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _vm.NewBrokerNavigation();
        }
    }
}