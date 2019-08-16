using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage : ContentPage
    {
        public IList<View> BasePageContent => BaseContent.Children;

        public BasePage()
        {
            InitializeComponent();
        }
    }
}