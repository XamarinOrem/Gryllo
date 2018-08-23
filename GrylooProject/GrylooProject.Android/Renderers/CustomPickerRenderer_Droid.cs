using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using GrylooProject.CustomControls;
using GrylooProject.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomPicker),
    typeof(CustomPickerRenderer_Droid))]
namespace GrylooProject.Droid.Renderers
{
   public  class CustomPickerRenderer_Droid : PickerRenderer
    {
        public CustomPickerRenderer_Droid(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
            Control?.SetHintTextColor(Android.Graphics.Color.Gray);
            if (e.OldElement == null && e.NewElement == null) return;
            Control.TextSize = 10f;
            Control.SetHeight(40);
            Control.SetPadding(0,0,0,0);


            //Control?.SetHintTextColor(Android.Graphics.Color.White);
            //if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            //{
            //    var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

            //    Control.Typeface = font;
            //}


        }


    }
}