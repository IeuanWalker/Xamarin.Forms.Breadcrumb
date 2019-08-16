using Xamarin.Forms.Xaml;

namespace DemoApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage2 : BasePage
    {
        public TestPage2()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TestPage3(), true);
        }
    }
}