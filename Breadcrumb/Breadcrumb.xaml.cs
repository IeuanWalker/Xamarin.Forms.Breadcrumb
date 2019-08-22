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

        // BreadcrumbBackgroundColor
        public static readonly BindableProperty BreadcrumbBackgroundColorProperty = BindableProperty.Create(
            nameof(BreadcrumbBackgroundColor),
            typeof(Color),
            typeof(Breadcrumb),
            Color.Transparent,
            defaultBindingMode: BindingMode.OneWay);

        public Color BreadcrumbBackgroundColor
        {
            get => (Color)GetValue(BreadcrumbBackgroundColorProperty);
            set => SetValue(BreadcrumbBackgroundColorProperty, value);
        }

        // LastBreadcrumbTextColor
        public static readonly BindableProperty LastBreadcrumbTextColorProperty = BindableProperty.Create(
            nameof(LastBreadcrumbTextColor),
            typeof(Color),
            typeof(Breadcrumb),
            Color.Black,
            defaultBindingMode: BindingMode.OneWay);

        public Color LastBreadcrumbTextColor
        {
            get => (Color)GetValue(LastBreadcrumbTextColorProperty);
            set => SetValue(LastBreadcrumbTextColorProperty, value);
        }

        // LastBreadcrumbCornerRadius
        public static readonly BindableProperty LastBreadcrumbCornerRadiusProperty = BindableProperty.Create(
            nameof(LastBreadcrumbCornerRadius),
            typeof(CornerRadius),
            typeof(Breadcrumb),
            new CornerRadius(10),
            defaultBindingMode: BindingMode.OneWay);

        public CornerRadius LastBreadcrumbCornerRadius
        {
            get => (CornerRadius)GetValue(LastBreadcrumbCornerRadiusProperty);
            set => SetValue(LastBreadcrumbCornerRadiusProperty, value);
        }

        // LastBreadcrumbBackgroundColor
        public static readonly BindableProperty LastBreadcrumbBackgroundColorProperty = BindableProperty.Create(
            nameof(LastBreadcrumbBackgroundColor),
            typeof(Color),
            typeof(Breadcrumb),
            Color.Transparent,
            defaultBindingMode: BindingMode.OneWay);

        public Color LastBreadcrumbBackgroundColor
        {
            get => (Color)GetValue(LastBreadcrumbBackgroundColorProperty);
            set => SetValue(LastBreadcrumbBackgroundColorProperty, value);
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

        // IsNavigationEnabled
        public static readonly BindableProperty IsNavigationEnabledProperty = BindableProperty.Create(
            nameof(IsNavigationEnabled),
            typeof(bool),
            typeof(Breadcrumb),
            true,
            defaultBindingMode: BindingMode.OneWay);

        public bool IsNavigationEnabled
        {
            get => (bool)GetValue(IsNavigationEnabledProperty);
            set => SetValue(IsNavigationEnabledProperty, value);
        }

        // TODO: Separator Icon, Image, Text

        #endregion Control properties

        private readonly INavigation _nav;

        public Breadcrumb()
        {
            InitializeComponent();

            _nav = Navigation;

            // Event fired the moment ContentView is displayed
            Device.BeginInvokeOnMainThread(() =>
            {
                // Get list of all pages in the NavigationStack that has a selectedPage title
                List<Page> pages = Navigation.NavigationStack.Select(x => x).Where(x => !string.IsNullOrEmpty(x?.Title)).ToList();

                // If any pages, make the control visible
                IsVisible = pages.Count > 0;

                // Get last selectedPage in stack
                Page lastPage = pages.LastOrDefault();

                // Loop all pages
                foreach (Page page in pages)
                {
                    if (!page.Equals(lastPage))
                    {
                        // Create breadcrumb
                       PancakeView breadCrumb1 = BreadCrumbLabelCreator(page);

                        // Add tap gesture
                        if (IsNavigationEnabled)
                        {
                            TapGestureRecognizer tapGesture = new TapGestureRecognizer
                            {
                                CommandParameter = page,
                                Command = new Command<Page>(async (item) => await GoBack(item))
                            };
                            breadCrumb1.GestureRecognizers.Add(tapGesture);
                        }

                        // Add breadcrumb and separator to BreadCrumbContainer
                        BreadCrumbContainer.Children.Add(breadCrumb1);
                        BreadCrumbContainer.Children.Add(SeparatorCreator());
                        continue;
                    }

                    // Add ChildAdded event to trigger animation
                    BreadCrumbContainer.ChildAdded += AnimatedStack_ChildAdded;

                    // Create selectedPage title label
                    PancakeView breadCrumb2 = BreadCrumbLabelCreator(page, true);

                    // Move BreadCrumb of selectedPage to start the animation
                    breadCrumb2.TranslationX = Application.Current.MainPage.Width;

                    // Add breadcrumb to container
                    BreadCrumbContainer.Children.Add(breadCrumb2);
                }
            });
        }

        /// <summary>
        /// Creates a new Breadcrumb object
        /// </summary>
        /// <param name="page"></param>
        /// <param name="isLast"></param>
        private PancakeView BreadCrumbLabelCreator(Page page, bool isLast = false)
        {
            // Create StackLayout to contain the label within a PancakeView
            StackLayout stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Create and Add label to StackLayout
            stackLayout.Children.Add(new Label
            {
                Text = page.Title,
                FontSize = 15,
                TextColor = isLast ? LastBreadcrumbTextColor : TextColor,
                VerticalTextAlignment = TextAlignment.Center
            });

            // Create PancakeView, and add StackLayout containing the selectedPage title
            return new PancakeView
            {
                Padding = 10,
                CornerRadius = isLast ? LastBreadcrumbCornerRadius : CornerRadius,
                BackgroundColor = isLast ? LastBreadcrumbBackgroundColor : BreadcrumbBackgroundColor,
                Content = stackLayout
            };
        }

        /// <summary>
        /// Creates a new Separator object
        /// </summary>
        private Label SeparatorCreator()
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

        /// <summary>
        /// Animates item added to stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimatedStack_ChildAdded(object sender, ElementEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                double width = Application.Current.MainPage.Width;

                Animation storyboard = new Animation();
                Animation enterRight = new Animation(d =>
                BreadCrumbContainer.Children.Last().TranslationX = d,
                width,
                0,
                Easing.Linear);
                storyboard.Add(0, 1, enterRight);
                storyboard.Commit(
                    BreadCrumbContainer.Children.Last(),
                    "RightToLeftAnimation", length: AnimationSpeed
                );
            });
        }

        /// <summary>
        /// Navigates the user back to chosen selectedPage in the Navigation stack
        /// </summary>
        /// <param name="selectedPage"></param>
        private async Task GoBack(Page selectedPage)
        {
            System.Console.WriteLine($"{nameof(GoBack)} Clicked");
            // Check if selectedPage is still in Navigation Stack
            if (!_nav.NavigationStack.Any(x => x.Equals(selectedPage)))
            {
                System.Console.WriteLine("Page not in stack");
                // PopToRoot triggered if selectedPage is missing from navigation stack
                await _nav.PopToRootAsync();
                System.Console.WriteLine("Pop to root");
                return;
            }

            System.Console.WriteLine("Page in stack");
            // Get all pages after and including selectedPage
            List<Page> pages = _nav.NavigationStack.SkipWhile(x => x != selectedPage).ToList();

            System.Console.WriteLine("List of pages after selected page -");
            pages.ForEach(x => System.Console.WriteLine(x.Title));
            // Remove selectedPage
            pages.Remove(selectedPage);

            System.Console.WriteLine("Selected page removed");

            // Remove current page (this will be removed with a PopAsync after all other relevant pages are removed)
            pages = pages.Take(pages.Count - 1).ToList();

            System.Console.WriteLine("Current page removed");

            // Remove all pages left in list (i.e. all pages after selectedPage, minus the current page)
            foreach (Page page in pages)
            {
                _nav.RemovePage(page);
            }

            System.Console.WriteLine("Pages inbetween selectedPage and CurrentPage removed");

            // Remove current page
            await _nav.PopAsync();

            System.Console.WriteLine("Popasync");
        }
    }
}