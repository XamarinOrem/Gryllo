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
using Xamarin.Forms;
using Android.Graphics;

using GrylooProject.Droid;
using Android.Graphics.Drawables;
using System.ComponentModel;

using Android.Views.InputMethods;

using GrylooProject.CustomControls;
using GrylooProject.Droid.Renderers;

[assembly: ExportRenderer(typeof(CustomEntryNext), typeof(EntryExtRenderer_Droid))]

namespace GrylooProject.Droid.Renderers
{
    public static class EnumExtensions
    {
        public static ImeAction GetValueFromDescription(this ReturnKeyTypes value)
        {
            var type = typeof(ImeAction);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == value.ToString())
                        return (ImeAction)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value.ToString())
                        return (ImeAction)field.GetValue(null);
                }
            }
            throw new NotSupportedException($"Not supported on Android: {value}");
        }
    }
    public class EntryExtRenderer_Droid : EntryRenderer
    {
        public EntryExtRenderer_Droid() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            var baseEntry = (CustomEntryNext)Element;

            base.OnElementChanged(e);
            //if (Control != null)
            //{
            //    Control.Background = Resources.GetDrawable(Resource.Drawable.RoundedEntry);
            //    Control.SetPadding(30, 25, 0, 25);
            //}

            //if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            //{
            //    var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

            //    Control.Typeface = font;
            //}

            if (!((Control != null) & (e.NewElement != null))) return;
            var entryExt = (e.NewElement as CustomEntryNext);
            if (entryExt == null) return;
            Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();

            Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), Control.ImeOptions);

            Control.EditorAction += (sender, args) =>
            {
                baseEntry.EntryActionFired();
            };
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomEntryNext.ReturnKeyPropertyName)
            {
                var entryExt = (sender as CustomEntryNext);
                Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();
                // This is hackie ;-) / A Android-only bindable property should be added to the EntryExt class 
                Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), Control.ImeOptions);
            }
        }

    }
}