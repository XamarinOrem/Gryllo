using System;

using Foundation;
using UIKit;

namespace GrylooProject.iOS
{
    public partial class TableViewCell2 : UITableViewCell
    {
        public static readonly NSString Key = new NSString("TableViewCell2");
        public static readonly UINib Nib;

        static TableViewCell2()
        {
            Nib = UINib.FromName("TableViewCell2", NSBundle.MainBundle);
        }

        protected TableViewCell2(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
