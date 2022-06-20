using System;
using System.Threading.Tasks;
using QuantTrade.Model.Interface;
using Xamarin.Forms;

namespace QuantTrade.Helpers
{
    // dependency services
    public interface IAuth
    {
        Task<bool> RegisterUser(string email, string password);
        Task<bool> LoginUser(string email, string password);
        bool IsAuthenticated();
        string GetCurrentUserId();
        UserInfo GetCurrentUserInfo();
    }
    public class Auth
    {
        // The DependencyService is getting a class that implement the IAuth interface
        private static IAuth auth = DependencyService.Get<IAuth>();
        
        public Auth()
        {
            
        }

        public static async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                return await auth.RegisterUser(email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return false;
            }
        }
        
        public static async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                // if user does not exist, register
                return await auth.LoginUser(email, password);
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return false;
            }

        }
        
        public static bool IsAuthenticated()
        {
            return auth.IsAuthenticated();
        }
        
        public static string GetCurrentUserId()
        {
            return auth.GetCurrentUserId();
        }
        
        public static UserInfo GetCurrentUserInfo()
        {
            return auth.GetCurrentUserInfo();
        }
        
    }
}