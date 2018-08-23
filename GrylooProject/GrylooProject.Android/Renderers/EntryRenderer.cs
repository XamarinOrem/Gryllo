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

[assembly: ExportRenderer(typeof(CustomEntry), typeof(EntryAndroidRenderer))]

namespace GrylooProject.Droid.Renderers
{
    public class EntryAndroidRenderer : EntryRenderer
    {
        public EntryAndroidRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

             Control.Background = null;

           
            

        }

    }
}