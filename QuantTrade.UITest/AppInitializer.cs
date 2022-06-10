using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace QuantTrade.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            switch (platform)
            {
                case Platform.Android:
                    return ConfigureApp
                        .Android
                        .InstalledApp("com.companyname.QuantTrade")
                        //.ApkFile(@"C:\ionictest\eanew\platforms\android\build\outputs\apk\android-debug.apk")
                        .StartApp();

                default:
                    throw new System.NotSupportedException();
            }
        }
    }
}