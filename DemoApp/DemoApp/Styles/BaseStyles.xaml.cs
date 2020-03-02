using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseStyles : ResourceDictionary
    {
        public BaseStyles()
        {
            InitializeComponent();
        }
    }
}