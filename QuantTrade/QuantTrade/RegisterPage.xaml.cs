using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuantTrade
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);
            
            QTLogo.Source = ImageSource.FromResource("QuantTrade.Assets.Images.QuantTrade.png", assembly); 
        }
    }
}