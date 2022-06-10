using System.Linq;
using QuantTrade.UITest.Util;
using QuantTrade.Utils.AutomationIdProps;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace QuantTrade.UITest.Pages
{
    public class RegisterPage : BasePage
    {
        readonly Query _registerLabel;
        readonly Query _loginButton;
        readonly Query _registerButton;
        readonly Query _emailEntry;
        readonly Query _passwordEntry;
        readonly Query _passwordVerifyEntry;
        readonly Query _emailLabel;
        readonly Query _passwordLabel;
        readonly Query _passwordVerifyLabel;
        
        public 
        
            RegisterPage(IApp app) : base(app, PageTitleConstants.RegisterPage)
        {
            _registerLabel = x => x.Marked(AutomationIdProps.RegisterPage_RegisterLabel);
            _emailEntry = x => x.Marked(AutomationIdProps.RegisterPage_EmailEntry);
            _passwordEntry = x => x.Marked(AutomationIdProps.RegisterPage_PasswordEntry);
            _passwordVerifyEntry = x => x.Marked(AutomationIdProps.RegisterPage_PasswordVerifyEntry);
            _emailLabel = x => x.Marked(AutomationIdProps.RegisterPage_EmailLabel);
            _passwordLabel = x => x.Marked(AutomationIdProps.RegisterPage_PasswordLabel);
            _passwordVerifyLabel = x => x.Marked(AutomationIdProps.RegisterPage_PasswordVerifyLabel);
            _loginButton = x => x.Marked(AutomationIdProps.RegisterPage_LoginButton);
            _registerButton = x => x.Marked(AutomationIdProps.RegisterPage_RegisterButton);
        }
        
        public override void WaitForPageToLoad()
        {
            App.WaitForElement(_registerLabel, "Main Screen Did Not Appear");
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
        
        public void EnterVerifyPassword(in string password)
        {
            EnterText(_passwordVerifyEntry, password);
            App.Screenshot($"Entered verify password: {password}");
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
        
        public string GetPasswordVerifyLabelText() => App.Query(_passwordVerifyLabel).First().Text;

        public bool GetRegisterButtonEnabled() => App.Query(_registerButton).First().Enabled;
        public string GetRegisterLabelText() => App.Query(_registerLabel).First().Text;
    }
}