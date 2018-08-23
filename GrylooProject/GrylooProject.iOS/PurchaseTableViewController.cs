﻿
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Xamarin.InAppPurchase;

namespace GrylooProject.iOS
{
    public partial class PurchaseTableViewController : UITableViewController, IPurchaseViewController
    {
        #region Private Variables
        private InAppPurchaseManager _purchaseManager;
        private UIStoryboard _Storyboard;
        #endregion

        #region Computed Properties
        /// <summary>
        /// Gets the purchase manager.
        /// </summary>
        /// <value>The purchase manager.</value>
        public InAppPurchaseManager PurchaseManager
        {
            get { return _purchaseManager; }
        }

        /// <summary>
        /// Gets the storyboard.
        /// </summary>
        /// <value>The storyboard.</value>
        public UIStoryboard Storyboard
        {
            get { return _Storyboard; }
        }

        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public PurchaseTableSource DataSource
        {
            get { return (PurchaseTableSource)TableView.Source; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InAppPurchaseTest.PurchaseTableViewController"/> class.
        /// </summary>
        /// <param name="handle">Handle.</param>
        public PurchaseTableViewController(IntPtr handle) : base(handle)
        {
        }
        #endregion

        #region Public Override Methods
        /// <Docs>Called when the system is running low on memory.</Docs>
        /// <summary>
        /// Dids the receive memory warning.
        /// </summary>
        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        /// <summary>
        /// Views the did load.
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Adjust for iOS 7
            TableView.ContentInset = new UIEdgeInsets(20, 0, 0, 0);
            TableView.ContentOffset = new System.Drawing.PointF(0, -20);

            // Register the TableView's data source
            TableView.Source = new PurchaseTableSource(this);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Ask the tableview to refresh it datasource and redisplay the new data.
        /// </summary>
        public void ReloadData()
        {

            // Ask datasource and table to reload
            DataSource.LoadData();
            TableView.ReloadData();

            // Update purchases count
            if (DataSource.puchasedProductCount == 0)
            {
                //PurchasesTab.BadgeValue = null;
            }
            else
            {
                // PurchasesTab.BadgeValue = DataSource.puchasedProductCount.ToString();
            }
        }

        /// <summary>
        /// Attachs to purchase manager.
        /// </summary>
        /// <param name="purchaseManager">Purchase manager.</param>
        public void AttachToPurchaseManager(UIStoryboard Storyboard, InAppPurchaseManager purchaseManager)
        {

            // Save connection
            _Storyboard = Storyboard;
            _purchaseManager = purchaseManager;

            // Respond to events
            _purchaseManager.ReceivedValidProducts += (products) => {
                // Received valid products from the iTunes App Store,
                // Update the display
                ReloadData();
            };

            _purchaseManager.InAppProductPurchased += (transaction, product) => {
                // Update list to remove any non-consumable products that were
                // purchased
                ReloadData();
            };

            _purchaseManager.InAppPurchaseProductQuantityConsumed += (identifier) => {
                // Update list to remove any consumable products that were
                // used up
                ReloadData();
            };

            _purchaseManager.InAppPurchasesRestored += (count) => {
                // Update list to remove any non-consumable products that were
                // purchased and restored
                if (count > 0) ReloadData();
            };

            _purchaseManager.TransactionObserver.InAppPurchaseContentDownloadFailed += (download) => {
                // If a download fails we have still purchased a product show we need to show it
                // on the list and show that it is still awaiting download.
                ReloadData();
            };

            // Display initial data
            ReloadData();
        }
        #endregion

    }
}