using Xamarin.Forms.Xaml;

namespace DemoApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage3 : BasePage
    {
        public TestPage3()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TestPage1(), true);
        }
    }
}