using System;
using GrylooProject.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WebView), typeof(webViewLine))]
namespace GrylooProject.iOS.Renderers
{
    public class webViewLine : Xamarin.Forms.Platform.iOS.WebViewRenderer
    {
    
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (NativeView != null)
            {
                var webView = (UIWebView)NativeView;

                webView.Opaque = false;
                webView.BackgroundColor = UIColor.Clear;
            }
        }
    }
}
