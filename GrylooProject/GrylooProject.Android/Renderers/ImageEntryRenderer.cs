using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GrylooProject.Droid.Renderers;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Support.V4.Content;
using GrylooProject.CustomControls;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace GrylooProject.Droid.Renderers
{
    public class ImageEntryRenderer : EntryRenderer
    {
        ImageEntry element;

        public ImageEntryRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            //
            var baseEntry = (ImageEntry)Element;
            if (!((Control != null) & (e.NewElement != null))) return;
            var entryExt = (e.NewElement as ImageEntry);
            if (entryExt == null) return;
            Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();

            Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), Control.ImeOptions);

            Control.EditorAction += (sender, args) =>
            {
                baseEntry.EntryActionFired();
            };
            //


            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (ImageEntry)this.Element;


            var editText = this.Control;
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
                        break;
                    case ImageAlignment.Right:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Image), null);
                        break;
                }
                
            }
            editText.CompoundDrawablePadding = 30;
            editText.TextAlignment = Android.Views.TextAlignment.Gravity;
            editText.VerticalFadingEdgeEnabled = true;


           

            Control.Background = null;
            // Control.Background.SetColorFilter(element.LineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);

          

        }

        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
        }



        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == ImageEntry.ReturnKeyPropertyName)
            {
                var entryExt = (sender as ImageEntry);
                Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();
                Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), Control.ImeOptions);
            }
        }

    }
}