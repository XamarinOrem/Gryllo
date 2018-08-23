using System;
using Foundation;
using GrylooProject.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Label), typeof(LegalNote))]
namespace GrylooProject.iOS.Renderers
{
    public class LegalNote:LabelRenderer
    {
        

      //protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        //{
        //    base.OnElementChanged(e);

        //    var view = (Label)Element;
        //    if (view == null) return;

        //    var attr = new NSAttributedStringDocumentAttributes();
        //    var nsError = new NSError();
        //    attr.DocumentType = NSDocumentType.HTML;

        //    Control.AttributedText = new NSAttributedString(view.Text, attr, ref nsError);
        //}
        }

}
