using Android.App;
using Android.Content;
using System.Threading.Tasks;
using Android.OS;
using System.Threading;
using QuantTrade.Helpers.Messages;
using QuantTrade.Helpers.Services;
using Xamarin.Forms;

namespace QuantTrade.Droid.Service
{
    [Service]
    public class RunUpdatingAccountsService : Android.App.Service
    {
        CancellationTokenSource _cts;

        public override IBinder OnBind (Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource ();

            Task.Run (() => {
                try {
                    //INVOKE THE SHARED CODE
                    var task = new UpdateAccountDataService();
                    task.RunUpdateAccounts(_cts.Token).Wait();
                }
                catch (OperationCanceledException) {
                }
                finally {
                    if (_cts.IsCancellationRequested) {
                        var message = new CancelMessage();
                        Device.BeginInvokeOnMainThread (
                            () => MessagingCenter.Send(message, "CancelledMessage")
                        );
                    }
                }

            }, _cts.Token);

            return StartCommandResult.Sticky;
        }
        

        public override void OnDestroy ()
        {
            if (_cts != null) {
                _cts.Token.ThrowIfCancellationRequested ();

                _cts.Cancel ();
            }
            base.OnDestroy ();
        }
    }
}