﻿using System;

using Foundation;
using UIKit;
using Xamarin.InAppPurchase;

namespace GrylooProject.iOS
{
    public partial class TableViewCell1 : UITableViewCell
    {
        public static readonly NSString Key = new NSString("TableViewCell1");
        public static readonly UINib Nib;
        #region Private Variables
        private InAppPurchaseManager _purchaseManager;
        private bool _wiredUp = false;
        private bool _isRestore = false;
        #endregion

        #region Computed Properties



        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>The product.</value>
        public InAppProduct Product { get; set; }

        /// <summary>
        /// Gets the purchase manager.
        /// </summary>
        /// <value>The purchase manager.</value>
        public InAppPurchaseManager PurchaseManager
        {
            get { return _purchaseManager; }
        }
        #endregion
        static TableViewCell1()
        {
            Nib = UINib.FromName("TableViewCell1", NSBundle.MainBundle);
        }
        public TableViewCell1() : base()
        {
        }
        protected TableViewCell1(IntPtr handle) : base(handle)
        {


            // Note: this .ctor should not contain any initialization logic.
        }
        #region Public Methods
        public void DisplayProduct(InAppPurchaseManager purchaseManager, InAppProduct product)
        {

            // Save copy of the current product and purchase manager
            _purchaseManager = purchaseManager;
            Product = product;

            // Set image based on the product type
            switch (product.ProductType)
            {
                case InAppProductType.NonConsumable:
                    if (product.Downloadable)
                    {
                        // ItemImage.Image = UIImage.FromFile("Images/Downloadable.png");
                    }
                    else
                    {
                        //  ItemImage.Image = UIImage.FromFile("Images/NonConsumable.png");
                    }
                    break;
                case InAppProductType.Consumable:
                    //ItemImage.Image = UIImage.FromFile("Images/Consumable.png");
                    break;
                case InAppProductType.AutoRenewableSubscription:
                    // ItemImage.Image = UIImage.FromFile("Images/Subscription.png");
                    break;
                case InAppProductType.FreeSubscription:
                    //ItemImage.Image = UIImage.FromFile("Images/FreeSubscription.png");
                    break;
                case InAppProductType.NonRenewingSubscription:
                    //ItemImage.Image = UIImage.FromFile("Images/NonRenewingSubscription.png");
                    break;
                case InAppProductType.Unknown:
                    //ItemImage.Image = UIImage.FromFile("Images/Unknown.png");
                    break;
            }

            // Fill in the rest of the information
            _isRestore = false;
            //ItemTitle.Text = product.Title;
            //DownloadProgress.Hidden = true;
            //ItemDescription.Text = product.Description;
            //ItemPrice.Text = product.FormattedPrice;
            //BuyButton.SetTitle("Buy", UIControlState.Normal);
            //BuyButton.Hidden = !_purchaseManager.CanMakePayments;
            //BuyButton.Enabled = true;

            // Wireup Button
            WireupBuyButton();
        }

        public void DisplayDownload(InAppPurchaseManager purchaseManager)
        {

            // Save copy of the current product and purchase manager
            _purchaseManager = purchaseManager;
            Product = purchaseManager.ProductDownloadingContent;

            // Has the product already been purged from memory?
            if (Product == null)
            {
                //// Inform user of completion
                //ItemImage.Image = UIImage.FromFile("Images/Downloadable.png");
                //ItemTitle.Text = "Finalizing Download";
                //ItemDescription.Text = "Completing product download and moving product into position.";
                //ItemPrice.Text = "100%";
                //BuyButton.Hidden = true;
                //DownloadProgress.Hidden = false;
                //DownloadProgress.Progress = 1.0f;
            }
            else
            {
                // Set image based on the product type
                switch (Product.ProductType)
                {
                    case InAppProductType.NonConsumable:
                        if (Product.Downloadable)
                        {
                            // ItemImage.Image = UIImage.FromFile("Images/Downloadable.png");
                        }
                        else
                        {
                            //  ItemImage.Image = UIImage.FromFile("Images/NonConsumable.png");
                        }
                        break;
                    case InAppProductType.Consumable:
                        //ItemImage.Image = UIImage.FromFile("Images/Consumable.png");
                        break;
                    case InAppProductType.AutoRenewableSubscription:
                        // ItemImage.Image = UIImage.FromFile("Images/Subscription.png");
                        break;
                    case InAppProductType.FreeSubscription:
                        // ItemImage.Image = UIImage.FromFile("Images/FreeSubscription.png");
                        break;
                    case InAppProductType.NonRenewingSubscription:
                        // ItemImage.Image = UIImage.FromFile("Images/NonRenewingSubscription.png");
                        break;
                    case InAppProductType.Unknown:
                        //  ItemImage.Image = UIImage.FromFile("Images/Unknown.png");
                        break;
                }

                // Fill in the rest of the information
                _isRestore = false;
                //ItemTitle.Text = string.Format("{0} v{1}", Product.Title, Product.DownloadableContentVersion);
                //ItemDescription.Text = Product.Description;
                //ItemPrice.Text = string.Format("{0:###}%", purchaseManager.ActiveDownloadPercent * 100.0f);
                //BuyButton.Hidden = true;
                //DownloadProgress.Hidden = false;
                //DownloadProgress.Progress = purchaseManager.ActiveDownloadPercent;
            }
        }

        /// <summary>
        /// Displays the restore previous purchases item
        /// </summary>
        /// <param name="purchaseManager">Purchase manager.</param>
        /// <param name="product">Product.</param>
        public void DisplayRestore(InAppPurchaseManager purchaseManager)
        {

            // Save copy of the current product and purchase manager
            _purchaseManager = purchaseManager;
            Product = null;

            // Fill in theg rest of the information
            _isRestore = true;
            //ItemImage.Image = UIImage.FromFile ("Images/RestorePurchases.png");
            //ItemTitle.Text = "Restore Purchases";
            //ItemDescription.Text = "Restore any previous purchases that you have made in this app.";
            //DownloadProgress.Hidden = true;
            //ItemPrice.Text = "";
            //BuyButton.SetTitle("Restore", UIControlState.Normal);
            //BuyButton.Hidden = !_purchaseManager.CanMakePayments;
            //BuyButton.Enabled = true;

            // Wireup Button
            WireupBuyButton();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Wireups the buy button.
        /// </summary>
        private void WireupBuyButton()
        {

            // Wireup the buy button
            if (!_wiredUp)
            {
                // Respond to the purchase button being pressed
                //BuyButton.TouchUpInside += (object sender, EventArgs e) => {

                //    // Nab sending button
                //    UIButton button = (UIButton)sender;

                //    // Is this a restore previous purchases button?
                //    if (_isRestore)
                //    {
                //        // Confirm request to purchase
                //        UIAlertView alert = new UIAlertView("Restore Purchase", "Do you want to restore previous purchases made from this app?", null, "Cancel", "Restore");
                //        //Wireup events
                //        alert.CancelButtonIndex = 0;
                //        alert.Clicked += (caller, buttonArgs) => {
                //            // Does the user want to restore?
                //            if (buttonArgs.ButtonIndex == 1)
                //            {
                //                // Yes, ask the purchase manager to try to restore any previous purchases
                //                button.Enabled = false;
                //                PurchaseManager.RestorePreviousPurchases();
                //            }
                //        };

                //        //Display dialog
                //        alert.Show();
                //    }
                //    else
                //    {
                //        // Confirm request to purchase
                //        UIAlertView alert = new UIAlertView("Purchase", String.Format("Do you want to buy {0} for {1}?", Product.Title, Product.FormattedPrice), null, "Cancel", "Buy");
                //        //Wireup events
                //        alert.CancelButtonIndex = 0;
                //        alert.Clicked += (caller, buttonArgs) => {
                //            // Does the user want to purchase?
                //            if (buttonArgs.ButtonIndex == 1)
                //            {
                //                //Yes, request purchase item	
                //                button.Enabled = false;
                //                _purchaseManager.BuyProduct(Product);
                //            }
                //        };

                //        //Display dialog
                //        alert.Show();
                //    }
                //};

                // Mark as wired up
                _wiredUp = true;
            }
        }
        #endregion
    }
}
