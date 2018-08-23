using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GrylooProject.CustomControls
{
    public enum ReturnKeyTypes : int
    {
        Default,
        Go,
        Google,
        Join,
        Next,
        Route,
        Search,
        Send,
        Yahoo,
        Done,
        EmergencyCall,
        Continue
    }
   public  class CustomEntryNext : Entry
    {
        public event EventHandler EventTriggered;

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnKeyType), typeof(ReturnKeyTypes), typeof(CustomEntryNext), ReturnKeyTypes.Done);

        public const string ReturnKeyPropertyName = "ReturnKeyType";

        public CustomEntryNext()
        {
            EventTriggered += Goto;
        }

        public CustomEntryNext Next { get; set; }


        public Command DoneCommand { get; set; }


        private static void Goto(object sender, EventArgs e)
        {
            ((CustomEntryNext)sender)?.Next?.Focus();

            ((CustomEntryNext)sender)?.DoneCommand?.Execute(null);
        }

        public ReturnKeyTypes ReturnKeyType
        {
            get { return (ReturnKeyTypes)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        public void EntryActionFired()
        {

            this.EventTriggered?.Invoke(this, null);
        }
    }
}


