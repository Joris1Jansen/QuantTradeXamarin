using System;
using System.Threading;
using System.Threading.Tasks;
using QuantTrade.Firestore;
using QuantTrade.Helpers.Messages;
using Xamarin.Forms;

namespace QuantTrade.Helpers.Services
{
    public class UpdateHoldingsDataService
    {
        public async Task RunUpdateHoldings(CancellationToken token)
        {
            await Task.Run(async () =>
            {
                token.ThrowIfCancellationRequested ();
                
                for (long i = 0; i < long.MaxValue; i++) {
                    token.ThrowIfCancellationRequested ();

                    var cbAccounts = await FireCBAccount.Read();
                    
                    foreach (var account in cbAccounts)
                    {
                        account.UpdateHoldings();
                    }

                    var message = new HoldingsUpdatedMessage();

                    Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send<HoldingsUpdatedMessage>(message, "HoldingsUpdatedMessage");
                    });
                    
                    await Task.Delay(120000);
                }
            }, token);
        }
    }
}