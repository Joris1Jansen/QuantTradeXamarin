using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace QuantTrade.CustomView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingSpinner : PopupPage
    {
        public LoadingSpinner()
        {
            InitializeComponent();
        }
    }
}