// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;

namespace GrylooProject.iOS
{
    [Register("PurchaseTableCell")]
    partial class PurchaseTableCell
    {
        [Outlet]
        UIKit.UILabel AvailableQuantity { get; set; }

        [Outlet]
        UIKit.UILabel ItemDescription { get; set; }

        [Outlet]
        UIKit.UIImageView ItemImage { get; set; }

        [Outlet]
        UIKit.UILabel ItemTitle { get; set; }

        [Outlet]
        UIKit.UIButton UpdateButton { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (AvailableQuantity != null)
            {
                AvailableQuantity.Dispose();
                AvailableQuantity = null;
            }

            if (ItemDescription != null)
            {
                ItemDescription.Dispose();
                ItemDescription = null;
            }

            if (ItemImage != null)
            {
                ItemImage.Dispose();
                ItemImage = null;
            }

            if (ItemTitle != null)
            {
                ItemTitle.Dispose();
                ItemTitle = null;
            }

            if (UpdateButton != null)
            {
                UpdateButton.Dispose();
                UpdateButton = null;
            }
        }
        
    }
}
