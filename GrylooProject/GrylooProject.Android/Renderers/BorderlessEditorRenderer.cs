using Xamarin.Forms.Platform.Android;
using GrylooProject.Droid.Renderers;
using Xamarin.Forms;
using GrylooProject.CustomControls;
using Android.Content;
using Android.Widget;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]

namespace GrylooProject.Droid.Renderers
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        public static void Init() { }

        public BorderlessEditorRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                
                SetPadding(0, 0, 0, 0);
            }
        }
    }
}