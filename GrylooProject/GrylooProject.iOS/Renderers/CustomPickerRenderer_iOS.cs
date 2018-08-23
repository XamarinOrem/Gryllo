using System;
using GrylooProject.CustomControls;
using GrylooProject.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer_iOS))]
namespace GrylooProject.iOS.Renderers
{
 public class CustomPickerRenderer_iOS:PickerRenderer
 {
  protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            Control.BorderStyle = UITextBorderStyle.None;
            Control.Font = UIFont.SystemFontOfSize(11f);
        }
    }
}