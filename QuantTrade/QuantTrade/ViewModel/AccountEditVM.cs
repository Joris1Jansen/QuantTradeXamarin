using System;
using QuantTrade.Firestore;
using QuantTrade.Model;
using Xamarin.Forms;

namespace QuantTrade.ViewModel
{
    public class AccountEditVM
    {
        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public BaseAccount EditAccount { get; set; }
        
        public AccountEditVM()
        {
            UpdateCommand = new Command<string>(Update, CanUpdate);
            DeleteCommand = new Command(Delete);
        }

        private async void Update(string newExperience)
        {
            EditAccount.Description = newExperience;
            
            bool result = await FireCBAccount.Update(EditAccount as CBAccount);
            if (result)
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Failure", "Experience failed to be deleted", "ok");
            }
        }

        private bool CanUpdate(string newExperience)
        {
            if (string.IsNullOrWhiteSpace(newExperience))
            {
                return false;
            }

            return true;
        }

        private async void Delete()
        {
            bool result = await FireCBAccount.Delete(EditAccount as CBAccount);
            if (result)
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Failure", "Experience failed to be deleted", "ok");
            }
        }
    }
}