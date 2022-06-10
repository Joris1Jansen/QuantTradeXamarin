using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using QuantTrade.Droid.Service;
using QuantTrade.Helpers.Messages;
using Xamarin.Forms;

namespace QuantTrade.Droid
{
    [Activity(Label = "QuantTrade", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Rg.Plugins.Popup.Popup.Init(this);

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            LoadApplication(new App());

            WireUpLongUpdatingHoldingsTask();
            WireUpLongUpdatingAccountsTask();
        }

        void WireUpLongUpdatingHoldingsTask()
        {
            //Update holdings task
            MessagingCenter.Subscribe<StartUpdatingHoldingsTask>(this, "StartUpdatingHoldingsTask", message =>
            {
                var intent = new Intent(this, typeof(RunUpdatingHoldingsService));
                StartService(intent);
            });

            MessagingCenter.Subscribe<StopUpdatingHoldingsTask>(this, "StopUpdatingHoldingsTask", message =>
            {
                var intent = new Intent(this, typeof(RunUpdatingHoldingsService));
                StopService(intent);
            });
        }

        void WireUpLongUpdatingAccountsTask()
        {
            //Update accounts task
            MessagingCenter.Subscribe<StartUpdatingAccountsTask>(this, "StartUpdatingAccountsTask", message =>
            {
                var intent = new Intent(this, typeof(RunUpdatingAccountsService));
                StartService(intent);
            });

            MessagingCenter.Subscribe<StopUpdatingAccountsTask>(this, "StopUpdatingAccountsTask", message =>
            {
                var intent = new Intent(this, typeof(RunUpdatingAccountsService));
                StopService(intent);
            });
        }
    }
}