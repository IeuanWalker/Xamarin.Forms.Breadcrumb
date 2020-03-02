using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DarkTheme : ResourceDictionary
    {
        public DarkTheme()
        {
            InitializeComponent();
        }
    }
}