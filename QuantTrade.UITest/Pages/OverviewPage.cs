using System.Linq;
using QuantTrade.UITest.Util;
using QuantTrade.Utils.AutomationIdProps;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace QuantTrade.UITest.Pages
{
    public class OverviewPage : BasePage
    {
        readonly Query _addBrokerButton;
        
        public 
        
            OverviewPage(IApp app) : base(app, PageTitleConstants.OverviewPage)
        {
            _addBrokerButton = x => x.Marked(AutomationIdProps.HomePage_AddBrokerButton);
        }
        
        public override void WaitForPageToLoad()
        {
            App.WaitForElement(_addBrokerButton, "Main Screen Did Not Appear");
        }
        
        // public void EnterEmail(in string email)
        // {
        //     EnterText(_emailEntry, email);
        //     App.Screenshot($"Entered email: {email}");
        // }

        // public string GetEmailLabelText() => App.Query(_emailLabel).First().Text;
    }
}