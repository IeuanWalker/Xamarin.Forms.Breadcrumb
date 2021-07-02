using Breadcrumb;
using Breadcrumb.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BreadcrumbButton), typeof(BreadcrumbButtonRenderer))]
namespace Breadcrumb.iOS
{
    public class BreadcrumbButtonRenderer :FrameRenderer
    {
        public new static void Init()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            AccessibilityTraits = UIAccessibilityTrait.Button;
        }

    }
}