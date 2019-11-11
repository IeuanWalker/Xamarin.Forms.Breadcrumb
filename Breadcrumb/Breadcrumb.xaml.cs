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

        // Separator
        public static readonly BindableProperty SeparatorProperty = BindableProperty.Create(nameof(Separator), typeof(ImageSource), typeof(Breadcrumb), new FontImageSource { Glyph = " / ", Color = Color.Black, Size = 15, });
        public ImageSource Separator
        {
            get => (ImageSource)GetValue(SeparatorProperty);
            set => SetValue(SeparatorProperty, value);
        }

        // FirstBreadCrumb
        public static readonly BindableProperty FirstBreadCrumbProperty = BindableProperty.Create(nameof(FirstBreadCrumb), typeof(ImageSource), typeof(Breadcrumb), null);
        public ImageSource FirstBreadCrumb
        {
            get => (ImageSource)GetValue(FirstBreadCrumbProperty);
            set => SetValue(FirstBreadCrumbProperty, value);
        }

        // Scrollbar Visibility
        public static readonly BindableProperty ScrollBarVisibilityProperty = BindableProperty.Create(nameof(ScrollBarVisibility), typeof(ScrollBarVisibility), typeof(Breadcrumb), ScrollBarVisibility.Never);
        public ScrollBarVisibility ScrollBarVisibility
        {
            get => (ScrollBarVisibility)GetValue(ScrollBarVisibilityProperty);
            set => SetValue(ScrollBarVisibilityProperty, value);
        }

        // Text Color
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Breadcrumb), Color.Black);
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        // Corner radius
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(Breadcrumb), new CornerRadius(10));
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        // BreadcrumbBackgroundColor
        public static readonly BindableProperty BreadcrumbBackgroundColorProperty = BindableProperty.Create(nameof(BreadcrumbBackgroundColor), typeof(Color), typeof(Breadcrumb), Color.Transparent);
        public Color BreadcrumbBackgroundColor
        {
            get => (Color)GetValue(BreadcrumbBackgroundColorProperty);
            set => SetValue(BreadcrumbBackgroundColorProperty, value);
        }

        // LastBreadcrumbTextColor
        public static readonly BindableProperty LastBreadcrumbTextColorProperty = BindableProperty.Create(nameof(LastBreadcrumbTextColor), typeof(Color), typeof(Breadcrumb), Color.Black);
        public Color LastBreadcrumbTextColor
        {
            get => (Color)GetValue(LastBreadcrumbTextColorProperty);
            set => SetValue(LastBreadcrumbTextColorProperty, value);
        }

        // LastBreadcrumbCornerRadius
        public static readonly BindableProperty LastBreadcrumbCornerRadiusProperty = BindableProperty.Create(nameof(LastBreadcrumbCornerRadius), typeof(CornerRadius), typeof(Breadcrumb), new CornerRadius(10));
        public CornerRadius LastBreadcrumbCornerRadius
        {
            get => (CornerRadius)GetValue(LastBreadcrumbCornerRadiusProperty);
            set => SetValue(LastBreadcrumbCornerRadiusProperty, value);
        }

        // LastBreadcrumbBackgroundColor
        public static readonly BindableProperty LastBreadcrumbBackgroundColorProperty = BindableProperty.Create(nameof(LastBreadcrumbBackgroundColor), typeof(Color), typeof(Breadcrumb), Color.Transparent);
        public Color LastBreadcrumbBackgroundColor
        {
            get => (Color)GetValue(LastBreadcrumbBackgroundColorProperty);
            set => SetValue(LastBreadcrumbBackgroundColorProperty, value);
        }

        // AnimationSpeed
        public static readonly BindableProperty AnimationSpeedProperty = BindableProperty.Create(nameof(AnimationSpeed), typeof(uint), typeof(Breadcrumb), (uint)800);
        public uint AnimationSpeed
        {
            get => (uint)GetValue(AnimationSpeedProperty);
            set => SetValue(AnimationSpeedProperty, value);
        }

        // IsNavigationEnabled
        public static readonly BindableProperty IsNavigationEnabledProperty = BindableProperty.Create(nameof(IsNavigationEnabled), typeof(bool), typeof(Breadcrumb), true);
        public bool IsNavigationEnabled
        {
            get => (bool)GetValue(IsNavigationEnabledProperty);
            set => SetValue(IsNavigationEnabledProperty, value);
        }

        #endregion Control properties
        public Breadcrumb()
        {
            InitializeComponent();

            // Event fired the moment ContentView is displayed
            Device.BeginInvokeOnMainThread(async () =>
            {
                // Get list of all pages in the NavigationStack that has a selectedPage title
                List<Page> pages = Navigation.NavigationStack.Select(x => x).Where(x => !string.IsNullOrEmpty(x?.Title)).ToList();

                // If any pages, make the control visible
                IsVisible = pages.Count > 0;

                // Loop all pages
                foreach (Page page in pages)
                {
                    if (!page.Equals(pages.LastOrDefault()))
                    {
                        // Create breadcrumb
                        PancakeView breadCrumb1 = BreadCrumbLabelCreator(page, false, page.Equals(pages.FirstOrDefault()));

                        // Add tap gesture
                        if (IsNavigationEnabled)
                        {
                            TapGestureRecognizer tapGesture = new TapGestureRecognizer
                            {
                                CommandParameter = page,
                                Command = new Command<Page>(async (item) => await GoBack(item).ConfigureAwait(false))
                            };
                            breadCrumb1.GestureRecognizers.Add(tapGesture);
                        }

                        // Add breadcrumb and separator to BreadCrumbContainer
                        BreadCrumbContainer.Children.Add(breadCrumb1);
                        BreadCrumbContainer.Children.Add(new Image
                        {
                            Source = Separator,
                            VerticalOptions = LayoutOptions.Center
                        });
                        continue;
                    }

                    // Add ChildAdded event to trigger animation
                    BreadCrumbContainer.ChildAdded += AnimatedStack_ChildAdded;

                    // Create selectedPage title label
                    PancakeView breadCrumb2 = BreadCrumbLabelCreator(page, true, page.Equals(pages.FirstOrDefault()));

                    // Move BreadCrumb of selectedPage to start the animation
                    breadCrumb2.TranslationX = Application.Current.MainPage.Width;

                    // Scroll to end of control
                    await Task.Delay(10);
                    await BreadCrumbsScrollView.ScrollToAsync(BreadCrumbContainer, ScrollToPosition.End, false);

                    // Add breadcrumb to container
                    BreadCrumbContainer.Children.Add(breadCrumb2);

                    // Scroll to last breadcrumb
                    await BreadCrumbsScrollView.ScrollToAsync(BreadCrumbContainer, ScrollToPosition.End, AnimationSpeed != 0);
                }
            });
        }

        /// <summary>
        /// Creates a new Breadcrumb object
        /// </summary>
        /// <param name="page"></param>
        /// <param name="isLast"></param>
        private PancakeView BreadCrumbLabelCreator(Page page, bool isLast, bool isFirst)
        {
            // Create StackLayout to contain the label within a PancakeView
            StackLayout stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Create and Add label to StackLayout
            if (isFirst && FirstBreadCrumb != null)
            {
                stackLayout.Children.Add(new Image
                {
                    Source = FirstBreadCrumb,
                    VerticalOptions = LayoutOptions.Center
                });
            }
            else
            {
                stackLayout.Children.Add(new Label
                {
                    Text = page.Title,
                    FontSize = 15,
                    TextColor = isLast ? LastBreadcrumbTextColor : TextColor,
                    VerticalOptions = LayoutOptions.Center,
                    VerticalTextAlignment = TextAlignment.Center
                });
            }


            // Create PancakeView, and add StackLayout containing the selectedPage title
            return new PancakeView
            {
                Padding = 10,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = isLast ? LastBreadcrumbCornerRadius : CornerRadius,
                BackgroundColor = isLast ? LastBreadcrumbBackgroundColor : BreadcrumbBackgroundColor,
                Content = stackLayout
            };
        }

        /// <summary>
        /// Animates item added to stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimatedStack_ChildAdded(object sender, ElementEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                // iOS scroll to end fix
                if (Device.RuntimePlatform.Equals(Device.iOS))
                {
                    await BreadCrumbsScrollView.ScrollToAsync(BreadCrumbContainer.Children.LastOrDefault(), ScrollToPosition.MakeVisible, false);
                }

                Animation lastBreadcrumbAnimation = new Animation
                {
                    { 0, 1, new Animation(_ => BreadCrumbContainer.Children.Last().TranslationX = _, Application.Current.MainPage.Width, 0, Easing.Linear) }
                };

                if (Device.RuntimePlatform.Equals(Device.iOS))
                {
                    Point point = BreadCrumbsScrollView.GetScrollPositionForElement(BreadCrumbContainer.Children.Last(), ScrollToPosition.End);
                    lastBreadcrumbAnimation.Add(0, 1, new Animation(_ => BreadCrumbsScrollView.ScrollToAsync(BreadCrumbContainer.Children.LastOrDefault(), ScrollToPosition.MakeVisible, true), BreadCrumbsScrollView.X, point.X - 6));
                }

                lastBreadcrumbAnimation.Commit(this, nameof(lastBreadcrumbAnimation), 16, AnimationSpeed);
            });
        }

        /// <summary>
        /// Navigates the user back to chosen selectedPage in the Navigation stack
        /// </summary>
        /// <param name="selectedPage"></param>
        private async Task GoBack(Page selectedPage)
        {
            // Check if selectedPage is still in Navigation Stack
            if (!Navigation.NavigationStack.Any(x => x == selectedPage))
            {
                // PopToRoot triggered if selectedPage is missing from navigation stack
                await Navigation.PopToRootAsync();
                return;
            }

            // Get all pages after and including selectedPage
            List<Page> pages = Navigation.NavigationStack.SkipWhile(x => x != selectedPage).ToList();

            // Remove selectedPage
            pages.Remove(selectedPage);

            // Remove current page (this will be removed with a PopAsync after all other relevant pages are removed)
            pages = pages.Take(pages.Count - 1).ToList();

            // Remove all pages left in list (i.e. all pages after selectedPage, minus the current page)
            foreach (Page page in pages)
            {
                Navigation.RemovePage(page);
            }

            // Remove current page
            await Navigation.PopAsync();
        }
    }
}