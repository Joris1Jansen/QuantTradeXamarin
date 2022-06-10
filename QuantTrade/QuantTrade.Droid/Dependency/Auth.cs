using System;
using System.Threading.Tasks;
using Firebase.Auth;
using QuantTrade.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(QuantTrade.Droid.Dependency.Auth))]
namespace QuantTrade.Droid.Dependency
{
    public class Auth : IAuth
    {
        public Auth()
        {
            
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);

                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthUserCollisionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception Ex)
            {
                throw new Exception("There was an unknown error");
            }
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidUserException Ex)
            {
                throw new Exception("There is no user record corresponding to these credentials");
            }
            catch(Exception Ex)
            {
                throw new Exception("There was an unknown error");
            }
        }

        public bool IsAuthenticated()
        {
            return FirebaseAuth.Instance.CurrentUser != null;
        }

        public string GetCurrentUserId()
        {
            return FirebaseAuth.Instance.CurrentUser.Uid;
        }
    }
}