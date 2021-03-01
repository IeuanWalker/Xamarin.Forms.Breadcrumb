using DemoApp.Styles;
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

        private void ThemeToggle_Clicked(object sender, System.EventArgs e)
        {
            if (ThemeToggle == null)
                return;

            switch (ThemeToggle.Text)
            {
                case nameof(ThemeEnum.Light):
                    Application.Current.Resources = new DarkTheme();
                    ThemeToggle.Text = nameof(ThemeEnum.Dark);
                    break;

                default:
                    Application.Current.Resources = new LightTheme();
                    ThemeToggle.Text = nameof(ThemeEnum.Light);
                    break;
            }
        }
    }

    public enum ThemeEnum
    {
        Light,
        Dark
    }
}