using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace QuantTrade.UITest.Tests
{
    [TestFixture(Platform.Android)]
    public abstract class BaseTest
    {
        readonly Platform _platform;

        IApp? _app;
        Pages.MainPage? _mainPage;

        Pages.OverviewPage? _overviewPage;

        Pages.RegisterPage? _registerPage;

        protected BaseTest(Platform platform) => _platform = platform;

        protected IApp App => _app ?? throw new NullReferenceException();
        protected Pages.MainPage MainPage => _mainPage ?? throw new NullReferenceException();
        protected Pages.OverviewPage OverviewPage => _overviewPage ?? throw new NullReferenceException();
        protected Pages.RegisterPage RegisterPage => _registerPage ?? throw new NullReferenceException();

        [SetUp]
        public virtual void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);

            // _firstPage = new FirstPage(App);
            _mainPage = new Pages.MainPage(App);
            _overviewPage = new Pages.OverviewPage(App);
            _registerPage = new Pages.RegisterPage(App);
            // _newUserSignUpPage = new NewUserSignUpPage(App);

            App.Screenshot("App Initialized");
            MainPage.WaitForPageToLoad();
        }
    }
}