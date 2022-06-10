// using Microsoft.EntityFrameworkCore;

using System.Linq;
using QuantTrade.UITest.Util;
using QuantTrade.Utils.AutomationIdProps;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace QuantTrade.UITest.Pages
{
    public class MainPage : BasePage
    {
        readonly Query _loginButton;
        readonly Query _registerButton;
        readonly Query _emailEntry;
        readonly Query _passwordEntry;
        readonly Query _emailLabel;
        readonly Query _passwordLabel;
        readonly Query _loginLabel;
        
        public 
        
        MainPage(IApp app) : base(app, PageTitleConstants.MainPage)
        {
            _emailEntry = x => x.Marked(AutomationIdProps.MainPage_EmailEntry);
            _passwordEntry = x => x.Marked(AutomationIdProps.MainPage_PasswordEntry);
            _loginButton = x => x.Marked(AutomationIdProps.MainPage_LoginButton);
            _registerButton = x => x.Marked(AutomationIdProps.MainPage_RegisterButton);
            _emailLabel = x => x.Marked(AutomationIdProps.MainPage_EmailLabel);
            _passwordLabel = x => x.Marked(AutomationIdProps.MainPage_PasswordLabel);
            _loginLabel = x => x.Marked(AutomationIdProps.MainPage_LoginLabel);
        }
        
        public override void WaitForPageToLoad()
        {
            App.WaitForElement(_emailEntry, "Main Screen Did Not Appear");
        }
        
        public void EnterEmail(in string email)
        {
            EnterText(_emailEntry, email);
            App.Screenshot($"Entered email: {email}");
        }

        public void EnterPassword(in string password)
        {
            EnterText(_passwordEntry, password);
            App.Screenshot($"Entered password: {password}");
        }

        public void PressRegisterButton()
        {
            App.Tap(_registerButton);
            App.Screenshot("Tapped Register Button");
        }
        
        public void PressLoginButton()
        {
            App.Tap(_loginButton);
            App.Screenshot("Tapped Login Button");
        }

        public string GetEmailLabelText() => App.Query(_emailLabel).First().Text;
        
        public string GetPasswordLabelText() => App.Query(_passwordLabel).First().Text;

        public bool GetLoginButtonEnabled() => App.Query(_loginButton).First().Enabled;
        
        public string GetLoginLabelText() => App.Query(_loginLabel).First().Text;
    }
}