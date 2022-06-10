using System;
using System.Threading;
using System.Threading.Tasks;
using QuantTrade.Firestore;
using QuantTrade.Helpers.Messages;
using Xamarin.Forms;

namespace QuantTrade.Helpers.Services
{
    public class UpdateAccountDataService
    {
        public async Task RunUpdateAccounts(CancellationToken token)
        {
            await Task.Run(async () =>
            {
                token.ThrowIfCancellationRequested ();
                
                for (long i = 0; i < long.MaxValue; i++) {
                    token.ThrowIfCancellationRequested ();
                    
                    await Task.Delay(5000);

                    var cbAccounts = await FireCBAccount.Read();
                    
                    foreach (var account in cbAccounts)
                    {
                        account.UpdateAccounts();
                    }

                    var message = new AccountsUpdatedMessage();

                    Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send<AccountsUpdatedMessage>(message, "AccountsUpdatedMessage");
                    });
                    
                    await Task.Delay(205000); 
                }
            }, token);
        }
    }
}