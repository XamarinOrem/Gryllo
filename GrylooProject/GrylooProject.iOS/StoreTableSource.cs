﻿using System;
using System.Drawing;
using Foundation;
using UIKit;
using Xamarin.InAppPurchase;
using System.Collections.Generic;

namespace GrylooProject.iOS
{
	public class StoreTableSource : UITableViewSource
	{
		#region Private Variables
		private StoreTableViewController _controller;
		private List<InAppProduct> products = new List<InAppProduct> ();
        #endregion
       private TableCellFactory<TableViewCell1> factory = new TableCellFactory<TableViewCell1>("CellID", "TableViewCell1");
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InAppPurchaseTest.PurchaseTableSource"/> class.
        /// </summary>
        /// <param name="controller">Controller.</param>
        public StoreTableSource (StoreTableViewController controller)
		{
			// Initialize
			_controller = controller;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Scans the Purchase Manage for products that have not already been purchased
		/// </summary>
		public  void LoadData()
        {

			// Anything to process?
			if (_controller.PurchaseManager == null)
				return;

			// Clear list
			products.Clear ();

			// Find all purchased products
			foreach (InAppProduct product in _controller.PurchaseManager) {

				// Take action based on the product type
				switch (product.ProductType) {
				case InAppProductType.Consumable:
					// Consumable products can always be purchased again
					products.Add (product);
					break;
				case InAppProductType.AutoRenewableSubscription:
				case InAppProductType.NonRenewingSubscription:
					// Only display if the subscription has expired
					if (product.SubscriptionExpired)
						products.Add (product);
					break;
				default:
					// Only display if the product hasn't been purchased
					if (!product.Purchased)
						products.Add (product);
					break;
				}
			}
		}
		#endregion 

		#region Public Override Methods
		/// <Docs>Table view displaying the sections.</Docs>
		/// <returns>Number of sections required to display the data. The default is 1 (a table must have at least one section).</returns>
		/// <para>Declared in [UITableViewDataSource]</para>
		/// <summary>
		/// Numbers the of sections.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		public override nint NumberOfSections (UITableView tableView)
		{
            try
            {
                // Are we currently downloading content?
                if (_controller.PurchaseManager.DownloadInProgress)
                {
                    // Add a third section for downloads
                    return 3;
                }
                else
                {
                    // Hard coded two sections: Available Products & My Account
                    return 2;
                }
            }
            catch
            {
                return 1;
            }
		}

		/// <Docs>Table view displaying the rows.</Docs>
		/// <summary>
		/// Rowses the in section.
		/// </summary>
		/// <returns>The in section.</returns>
		/// <param name="tableview">Tableview.</param>
		/// <param name="section">Section.</param>
		public override nint RowsInSection (UITableView tableview, nint section)
		{

			// Set value based on the section
			switch (section) {
			case 0:
				// Return the number of products
				return products.Count;
				break;
			}

			// Default to one
			return 1;
		}

		/// <Docs>Table view containing the section.</Docs>
		/// <summary>
		/// Called to populate the header for the specified section.
		/// </summary>
		/// <see langword="null"></see>
		/// <returns>The for header.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override string TitleForHeader (UITableView tableView, nint section)
		{
			// Set value based on the section
			switch (section) {
			case 0:
				// Products Header
				return "Available Products";
				break;
			case 1:
				// Advanced features
				return "My Account";
				break;
			case 2:
				// Downloads
				return "Downloading Content";
				break;
			}

			// Default to nothing
			return "";
		}

		/// <Docs>Table view containing the section.</Docs>
		/// <summary>
		/// Called to populate the footer for the specified section.
		/// </summary>
		/// <see langword="null"></see>
		/// <returns>The for footer.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override string TitleForFooter (UITableView tableView, nint section)
		{
			// Not displaying a footer
			return "";
		}

		/// <Docs>Table view requesting the cell.</Docs>
		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// Get table cell to hold data
			//var cell = tableView.DequeueReusableCell (StoreTableCell.Key) as StoreTableCell;
           var  cell = tableView.DequeueReusableCell(TableViewCell1.Key) as TableViewCell1;

            cell = factory.GetCell(tableView);

            // Take action based on the section
            switch (indexPath.Section) {
			case 0:
				// Get product for given cell
				InAppProduct product = products [indexPath.Row];

				//// Populate cell with product information
			cell.DisplayProduct (_controller.PurchaseManager, product);
				break;
			case 1:
				//// Display the link to restore purchases
				cell.DisplayRestore (_controller.PurchaseManager);
				break;
			case 2:
				//// Display the currently downloading item
			cell.DisplayDownload (_controller.PurchaseManager);
				break;
			}

			return cell;
		}
        //public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        //{
        //    var cell = factory.GetCell(tableView);
        //    cell._offerData = offerList[indexPath.Row];
        //    cell.TableView = tableView;
        //    cell.IndexPath = indexPath;
        //    cell.ParentController = parentController;
        //    cell.default_offer_expiry_days = default_offer_expiry_days;
        //    cell.minimum_offer_expire_days = minimum_offer_expire_days;
        //    cell.offer_expire_description = offer_expire_description;
        //    cell.sign_statement = sign_statement;
        //    cell.sign_plus_ccCheck_statement = sign_plus_ccCheck_statement;

        //    return cell;
        //}

        #endregion
    }
}

