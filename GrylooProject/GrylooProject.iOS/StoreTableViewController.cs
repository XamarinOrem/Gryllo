﻿
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Xamarin.InAppPurchase;

namespace GrylooProject.iOS
{
    public partial class StoreTableViewController : UITableViewController, IPurchaseViewController
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
        public StoreTableSource DataSource
        {
            get { return (StoreTableSource)TableView.Source; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InAppPurchaseTest.PurchaseTableViewController"/> class.
        /// </summary>
        /// <param name="handle">Handle.</param>
        public StoreTableViewController(IntPtr handle) : base(handle)
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
            TableView.Source = new StoreTableSource(this);
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

            _purchaseManager.InAppPurchasesRestored += (count) => {
                // Update list to remove any non-consumable products that were
                // purchased and restored
                ReloadData();
            };

            _purchaseManager.TransactionObserver.InAppPurchaseContentDownloadInProgress += (download) => {
                // Update the table to display the status of any downloads of hosted content
                // that we currently have in progress so we are forcing a table reload on the
                // download progress update. Since the final message will be the raising of the
                // InAppProductPurchased event, we'll just trap it to clear any completed
                // downloads instead of listening to the InAppPurchaseContentDownloadCompleted on the
                // purchase managers transaction observer.
                ReloadData();

                // Display download percent in the badge
                // StoreTab.BadgeValue = string.Format("{0:###}%", _purchaseManager.ActiveDownloadPercent * 100.0f); ;
            };

            _purchaseManager.TransactionObserver.InAppPurchaseContentDownloadCompleted += (download) => {
                // Clear badge
                // StoreTab.BadgeValue = null;
            };

            _purchaseManager.TransactionObserver.InAppPurchaseContentDownloadCanceled += (download) => {
                // Clear badge
                // StoreTab.BadgeValue = null;
            };

            _purchaseManager.TransactionObserver.InAppPurchaseContentDownloadFailed += (download) => {
                // Clear badge
                //StoreTab.BadgeValue = null;
            };

            _purchaseManager.TransactionObserver.InAppPurchaseContentDownloadFailed += (download) => {
                // Inform the user that the download has failed. Normally download would contain
                // information about the failure that you would want to display to the user, since
                // we are running in simulation mode download will be null, so just display a 
                // generic failure message.
                using (var alert = new UIAlertView("Download Failed", "Unable to complete the downloading of content for the product being purchased. Please try again later.", null, "OK", null))
                {
                    alert.Show();
                }

                // Force the table to reload to remove current download message
                ReloadData();
            };

            _purchaseManager.InAppProductPurchaseFailed += (transaction, product) => {
                // Inform caller that the purchase of the requested product failed.
                // NOTE: The transaction will normally encode the reason for the failure but since
                // we are running in the simulated iTune App Store mode, no transaction will be returned.
                //Display Alert Dialog Box
                using (var alert = new UIAlertView("Xamarin.InAppPurchase", String.Format("Attempt to purchase {0} has failed: {1}", product.Title, transaction.Error.ToString()), null, "OK", null))
                {
                    alert.Show();
                }

                // Force a reload to clear any locked items
                ReloadData();
            };

            // Initially populate the table with information
            ReloadData();
        }
        #endregion

    }
}