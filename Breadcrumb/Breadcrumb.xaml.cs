using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace Breadcrumb
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Breadcrumb : ContentView
    {
        #region Control properties

        // Text Color
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(Breadcrumb),
            Color.Black,
            defaultBindingMode: BindingMode.OneWay);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        // Corner radius
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(Breadcrumb),
            new CornerRadius(10),
            defaultBindingMode: BindingMode.OneWay);

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        // BreadCrumbBackgroundColor
        public static readonly BindableProperty BreadCrumbBackgroundColorProperty = BindableProperty.Create(
            nameof(BreadCrumbBackgroundColor),
            typeof(Color),
            typeof(Breadcrumb),
            Color.Transparent,
            defaultBindingMode: BindingMode.OneWay);

        public Color BreadCrumbBackgroundColor
        {
            get => (Color)GetValue(BreadCrumbBackgroundColorProperty);
            set => SetValue(BreadCrumbBackgroundColorProperty, value);
        }

        // LastBreadCrumbTextColor
        public static readonly BindableProperty LastBreadCrumbTextColorProperty = BindableProperty.Create(
            nameof(LastBreadCrumbTextColor),
            typeof(Color),
            typeof(Breadcrumb),
            Color.Black,
            defaultBindingMode: BindingMode.OneWay);

        public Color LastBreadCrumbTextColor
        {
            get => (Color)GetValue(LastBreadCrumbTextColorProperty);
            set => SetValue(LastBreadCrumbTextColorProperty, value);
        }

        // LastBreadCrumbCornerRadius
        public static readonly BindableProperty LastBreadCrumbCornerRadiusProperty = BindableProperty.Create(
            nameof(LastBreadCrumbCornerRadius),
            typeof(CornerRadius),
            typeof(Breadcrumb),
            new CornerRadius(10),
            defaultBindingMode: BindingMode.OneWay);

        public CornerRadius LastBreadCrumbCornerRadius
        {
            get => (CornerRadius)GetValue(LastBreadCrumbCornerRadiusProperty);
            set => SetValue(LastBreadCrumbCornerRadiusProperty, value);
        }

        // LastBreadCrumbBackgroundColor
        public static readonly BindableProperty LastBreadCrumbBackgroundColorProperty = BindableProperty.Create(
            nameof(LastBreadCrumbBackgroundColor),
            typeof(Color),
            typeof(Breadcrumb),
            Color.Transparent,
            defaultBindingMode: BindingMode.OneWay);

        public Color LastBreadCrumbBackgroundColor
        {
            get => (Color)GetValue(LastBreadCrumbBackgroundColorProperty);
            set => SetValue(LastBreadCrumbBackgroundColorProperty, value);
        }

        // AnimationSpeed
        public static readonly BindableProperty AnimationSpeedProperty = BindableProperty.Create(
            nameof(AnimationSpeed),
            typeof(uint),
            typeof(Breadcrumb),
            (uint)800,
            defaultBindingMode: BindingMode.OneWay);

        public uint AnimationSpeed
        {
            get => (uint)GetValue(AnimationSpeedProperty);
            set => SetValue(AnimationSpeedProperty, value);
        }

        // TODO: Seporator Icon, Image, Text

        #endregion Control properties

        public Breadcrumb()
        {
            InitializeComponent();

            // Event fired the moment contentview is displayed
            Device.BeginInvokeOnMainThread(() =>
            {
                List<Page> pageTitles = Navigation.NavigationStack.Select(x => x).Where(x => !string.IsNullOrEmpty(x?.Title)).ToList();
                Page lastPage = pageTitles?.LastOrDefault();
                foreach (Page page in pageTitles)
                {
                    IsVisible = true;

                    if (!page.Equals(lastPage))
                    {
                        // Create breadcrumb
                        PancakeView breadCrumb1 = BreadCrumbLabelCreator(page);

                        // Add tap gesture
                        TapGestureRecognizer tapGesture = new TapGestureRecognizer
                        {
                            CommandParameter = page,
                            Command = new Command<Page>(async (item) => await GoBack(item))
                        };
                        breadCrumb1.GestureRecognizers.Add(tapGesture);

                        // Add breadcrumb and seperator to BreadCrumbContainer
                        BreadCrumbContainer.Children.Add(breadCrumb1);
                        BreadCrumbContainer.Children.Add(SeperatorCreator());
                        continue;
                    }

                    // Add ChildAdded event to trigger animation
                    BreadCrumbContainer.ChildAdded += AnimatedStack_ChildAdded;

                    // Create page title label
                    PancakeView breadCrumb2 = BreadCrumbLabelCreator(page, true);

                    // Move BreadCrumb of page to start the animation
                    breadCrumb2.TranslationX = Application.Current.MainPage.Width;

                    // Add breadcrumb to container
                    BreadCrumbContainer.Children.Add(breadCrumb2);
                }
            });
        }

        private PancakeView BreadCrumbLabelCreator(Page page, bool isLast = false)
        {
            // Create Label
            Label label = new Label
            {
                Text = page.Title,
                FontSize = 15,
                TextColor = isLast ? LastBreadCrumbTextColor : TextColor,
                VerticalTextAlignment = TextAlignment.Center
            };

            // Create StackLayout to contain the label within a PancakeView
            StackLayout stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add label to stacklayout
            stackLayout.Children.Add(label);

            // Create PancakeView
            PancakeView pancakeView = new PancakeView
            {
                Padding = 10,
                CornerRadius = isLast ? LastBreadCrumbCornerRadius : CornerRadius,
                BackgroundColor = isLast ? LastBreadCrumbBackgroundColor : BreadCrumbBackgroundColor
            };

            // Add StackLayout containing label to PancakeView
            pancakeView.Content = stackLayout;

            return pancakeView;
        }

        private Label SeperatorCreator()
        {
            return new Label
            {
                Text = " / ",
                FontSize = 15,
                TextColor = TextColor,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };
        }

        private void AnimatedStack_ChildAdded(object sender, ElementEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                double width = Application.Current.MainPage.Width;

                Animation storyboard = new Animation();
                Animation enterRight = new Animation(callback: d =>
                BreadCrumbContainer.Children.Last().TranslationX = d,
                start: width,
                end: 0,
                easing: Easing.Linear);
                storyboard.Add(0, 1, enterRight);
                storyboard.Commit(
                BreadCrumbContainer.Children.Last(),
                "RightToLeftAnimation", length: AnimationSpeed);
            });
        }

        private async Task GoBack(Page page)
        {
            IEnumerable<Page> pageInStack = Navigation.NavigationStack.Where(x => x.Equals(page));
            if (pageInStack != null)
            {
                // Get all pages after selected page
                List<Page> test = Navigation.NavigationStack.SkipWhile(x => x != page).ToList();

                // Remove chosen page
                test.Remove(page);

                // Remove last page - current page (popasync will remove this page)
                test = test.Take(test.Count - 1).ToList();

                foreach (Page pageToRemove in test)
                {
                    Navigation.RemovePage(pageToRemove);
                }

                await Navigation.PopAsync();
            }
            else
            {
                await Navigation.PopToRootAsync();
            }
        }
    }
}