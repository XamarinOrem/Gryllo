using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Xamarin.Forms.Platform.Android.AppCompat;
using Java.Lang;
using GrylooProject.Droid.Renderers;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationBarRenderer))]


namespace GrylooProject.Droid.Renderers
{
    public class CustomNavigationBarRenderer : NavigationPageRenderer
    {
        public CustomNavigationBarRenderer(Context context) : base(context)
        {

        }


        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            try
            {
                var toolbar1 = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                try
                {
                    base.OnLayout(changed, l, t, r, b);
                }
                catch (Exception)
                {
                }

                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

                for (int index = 0; index < toolbar.ChildCount; index++)
                {
                    if (toolbar.GetChildAt(index) is TextView)
                    {
                        var title = toolbar.GetChildAt(index) as TextView;
                        float toolbarCenter = toolbar.MeasuredWidth / 2;
                        float titleCenter = title.MeasuredWidth / 2;
                        title.SetX(toolbarCenter - titleCenter);
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}