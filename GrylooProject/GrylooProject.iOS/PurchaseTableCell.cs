﻿using System;

using Foundation;
using UIKit;
using Xamarin.InAppPurchase;

namespace GrylooProject.iOS
{
    public partial class PurchaseTableCell : UITableViewCell
    {
        #region Private Variables
        private PurchaseTableViewController _controller;
        private InAppPurchaseManager _purchaseManager;
        private bool _wiredUp = false;
        private bool _displayContent = false;
        #endregion

        #region Computed Properties
        /// <summary>
        /// The key used it get the Prototype Cell from the storyboard.
        /// </summary>
        public static readonly NSString Key = new NSString("PurchaseTableCell");

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

        #region Constructors
        public PurchaseTableCell(IntPtr handle) : base(handle)
        {
        }
        #endregion

        #region Public Methods
        public void DisplayProduct(PurchaseTableViewController controller, InAppPurchaseManager purchaseManager, InAppProduct product)
        {

            // Save copy of the current product and purchase manager
            _controller = controller;
            _purchaseManager = purchaseManager;
            Product = product;

            // Set image based on the product type
            switch (product.ProductType)
            {
                case InAppProductType.NonConsumable:
                    if (product.Downloadable)
                    {
                        ItemImage.Image = UIImage.FromFile("Images/Downloadable.png");
                    }
                    else
                    {
                        ItemImage.Image = UIImage.FromFile("Images/NonConsumable.png");
                    }
                    break;
                case InAppProductType.Consumable:
                    ItemImage.Image = UIImage.FromFile("Images/Consumable.png");
                    break;
                case InAppProductType.AutoRenewableSubscription:
                    ItemImage.Image = UIImage.FromFile("Images/Subscription.png");
                    break;
                case InAppProductType.FreeSubscription:
                    ItemImage.Image = UIImage.FromFile("Images/FreeSubscription.png");
                    break;
                case InAppProductType.NonRenewingSubscription:
                    ItemImage.Image = UIImage.FromFile("Images/NonRenewingSubscription.png");
                    break;
                case InAppProductType.Unknown:
                    ItemImage.Image = UIImage.FromFile("Images/Unknown.png");
                    break;
            }

            // Fill in the rest of the information
            ItemTitle.Text = product.Title;
            ItemDescription.Text = product.Description;
            UpdateButton.Hidden = true;

            // Take action based on the product type
            switch (Product.ProductType)
            {
                case InAppProductType.Consumable:
                    // Show remaining quantity
                    AvailableQuantity.Hidden = false;
                    AvailableQuantity.Text = String.Format("{0} qty", product.AvailableQuantity);
                    break;
                case InAppProductType.AutoRenewableSubscription:
                case InAppProductType.NonRenewingSubscription:
                    // Force this product to use a calculated date
                    product.UseCalculatedExpirationDate = true;

                    // Show expiration date
                    AvailableQuantity.Hidden = false;
                    AvailableQuantity.Text = String.Format("Exp {0:d}", product.SubscriptionExpirationDate);
                    break;
                case InAppProductType.FreeSubscription:
                    // Show it never expires
                    AvailableQuantity.Hidden = false;
                    AvailableQuantity.Text = "Unlimited";
                    break;
                case InAppProductType.NonConsumable:
                    if (Product.Downloadable)
                    {
                        // Use quantity to show download state
                        if (Product.NewContentAvailable)
                        {
                            AvailableQuantity.Text = string.Format("v{0} Available", Product.DownloadableContentVersion);

                            // Display update button and wire it up
                            _displayContent = false;
                            UpdateButton.Hidden = _purchaseManager.DownloadInProgress;
                            UpdateButton.SetTitle("Update", UIControlState.Normal);
                            WireupUpdateButton();
                        }
                        else if (Product.ContentDownloaded)
                        {
                            AvailableQuantity.Text = string.Format("Ready v{0}", Product.DownloadableContentVersion);

                            // Display button
                            _displayContent = true;
                            UpdateButton.Hidden = false;
                            UpdateButton.SetTitle("Show", UIControlState.Normal);
                            WireupUpdateButton();
                        }
                        else
                        {
                            AvailableQuantity.Text = "Awaiting Content";

                            // Display update button and wire it up
                            _displayContent = false;
                            UpdateButton.Hidden = _purchaseManager.DownloadInProgress;
                            UpdateButton.SetTitle("Get", UIControlState.Normal);
                            WireupUpdateButton();
                        }
                        AvailableQuantity.Hidden = false;
                    }
                    else
                    {
                        // Not downloadable, hide quantity
                        AvailableQuantity.Hidden = true;
                    }
                    break;
                default:
                    AvailableQuantity.Hidden = true;
                    break;
            }

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Wireups the buy button.
        /// </summary>
        private void WireupUpdateButton()
        {

            // Wireup the buy button
            if (!_wiredUp)
            {
                // Respond to the purchase button being pressed
                UpdateButton.TouchUpInside += (object sender, EventArgs e) => {
                    // Nab sending button
                    UIButton button = (UIButton)sender;

                    // Displaying content?
                    if (_displayContent)
                    {
                        // Gain access to Product Content view
                        //var Storyboard = AppDelegate.Storyboard;
                        //ProductContent content = Storyboard.InstantiateViewController ("ProductContent") as ProductContent;

                        // Attach to product
                        //content.manager = _purchaseManager;
                        //content.product = Product;

                        // Display view
                        //_controller.PresentViewController(content,true,null);
                    }
                    else
                    {
                        // No, hide button
                        button.Hidden = true;

                        // Does the purchase manager "know" about this product?
                        if (_purchaseManager.SimulateiTunesAppStore && !_purchaseManager.SimulatedRestoredPurchaseProducts.Contains(Product.ProductIdentifier))
                        {
                            // No, quickly add it to the list of products to restore
                            // This is a hack for testing simulated downloads 
                            if (_purchaseManager.SimulatedRestoredPurchaseProducts == "")
                            {
                                _purchaseManager.SimulatedRestoredPurchaseProducts = Product.ProductIdentifier;
                            }
                            else
                            {
                                _purchaseManager.SimulatedRestoredPurchaseProducts += "," + Product.ProductIdentifier;
                            }
                        }

                        // Ask the purchase manager to restore previous purchases this is currently the
                        // only way to update content or request content be downloaded again from the
                        // Apple iTunes App Store. The purhcase manager will skip over any content it
                        // already has.
                        _purchaseManager.RestorePreviousPurchases();
                    }
                };

                // Mark as wired up
                _wiredUp = true;
            }
        }
        #endregion
    }
}
