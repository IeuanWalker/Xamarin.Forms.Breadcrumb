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

        // TODO: Seporator Icon, Image, Text

        #endregion Control properties

        public Breadcrumb()
        {
            InitializeComponent();

            // Event fired the moment ContentView is displayed
            Device.BeginInvokeOnMainThread(() =>
            {
                // Get list of all pages in the NavigationStack that has a page title
                IEnumerable<Page> pages = Navigation.NavigationStack.Select(x => x).Where(x => !string.IsNullOrEmpty(x?.Title));

                // If any pages, make the control visible
                IsVisible = pages.Any();

                // Get last page in stack
                Page lastPage = pages?.LastOrDefault();

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

                    // Create page title label
                    PancakeView breadCrumb2 = BreadCrumbLabelCreator(page, true);

                    // Move BreadCrumb of page to start the animation
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

            // Create PancakeView, and add StackLayout containing the page title
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

        /// <summary>
        /// Navigates the user back to chosen page in the Navigation stack
        /// </summary>
        /// <param name="page"></param>
        private async Task GoBack(Page page)
        {
            // Check if page is still in Navigation Stack
            if (Navigation.NavigationStack.Any(x => x.Equals(page)))
            {
                // Get all pages after selected page
                List<Page> pages = Navigation.NavigationStack.SkipWhile(x => x != page).ToList();

                // Remove chosen page
                pages.Remove(page);

                // Remove last page - current page (PopAsync will remove this page)
                pages = pages.Take(pages.Count - 1).ToList();

                // Remove all pages after chosen page
                foreach (Page pageToRemove in pages)
                {
                    Navigation.RemovePage(pageToRemove);
                }

                // Remove current page
                await Navigation.PopAsync();
            }
            else
            {
                // PopToRoot triggered if chosen page is missing from navigation stack
                await Navigation.PopToRootAsync();
            }
        }
    }
}