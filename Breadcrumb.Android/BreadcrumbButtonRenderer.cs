using Android.Content;
using Android.Views.Accessibility;
using Breadcrumb.Android;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView.Droid;
using AndroidView = Android.Views;
using Breadcrumb;

[assembly: ExportRenderer(typeof(BreadcrumbButton), typeof(BreadcrumbButtonRenderer))]
namespace Breadcrumb.Android
{
    public class BreadcrumbButtonRenderer : PancakeViewRenderer
    {
        public BreadcrumbButtonRenderer(Context context) : base(context)
        {
             SetAccessibilityDelegate(new MyAccessibilityDelegate());
        }
        private class MyAccessibilityDelegate : AccessibilityDelegate
        {
            public override void OnInitializeAccessibilityNodeInfo(AndroidView.View host, AccessibilityNodeInfo info)
            {
                base.OnInitializeAccessibilityNodeInfo(host, info);
                info.ClassName = "android.widget.Button";
                info.Clickable = true;
            }
        }
    }
}