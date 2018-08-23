using System;
using UIKit;
using Xamarin.Forms;
using GrylooProject.DependencyInterface;
using GrylooProject.iOS.DependencyInterface;
using Xamarin.InAppPurchase;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using System.Threading.Tasks;
using GrylooProject.Views;
using Rg.Plugins.Popup.Extensions;

[assembly: Dependency(typeof(PremiumMembership_iOS))]
namespace GrylooProject.iOS.DependencyInterface
{

    
    public class PremiumMembership_iOS : IPremiumMembership
    {
        string productId = "com.orem.grylloo.purchase";

        
        public static InAppPurchaseManager PurchaseManager = new InAppPurchaseManager();

        async Task<AppPurchase> IPremiumMembership.MakePayment(string keyId)
        {

            AppPurchase result = new AppPurchase();
            try
            {
                var connected = await CrossInAppBilling.Current.ConnectAsync();
                //try to purchase item
                var purchase = await CrossInAppBilling.Current.PurchaseAsync(keyId, ItemType.Subscription, "apppayload");
                if (purchase == null)
                {
                    //Not purchased
                    result.IsPaid = false;
                }
                else
                {
                    //Purchased!
                    result.IsPaid = true;
                }
            }
            catch (Exception ex)
            {
                //Something went wrong :()
                result.IsPaid = false;
            }
            finally
            {
                
                await CrossInAppBilling.Current.DisconnectAsync();
               // result.IsPaid = false;
            }
           
            return result;
        }
         
        public void GetPremiumMembership()
        {
            IntPtr ff = new IntPtr();

            ViewController1 _ViewController1 = new iOS.ViewController1(ff);
            try
            {  // Assembly public key
                string value = Xamarin.InAppPurchase.Utilities.Security.Unify(
                    new string[] { "1322f985c2",
                    "a34166b24",
                    "ab2b367",
                    "851cc6" },
                    new int[] { 0, 1, 2, 3 });


                // Initialize the In App Purchase Manager
#if SIMULATED
			PurchaseManager.SimulateiTunesAppStore = true;
#else
                PurchaseManager.SimulateiTunesAppStore = false;
#endif
                PurchaseManager.PublicKey = value;
                PurchaseManager.ApplicationUserName = "KMullins";

                // Warn user that the store is not available
                if (PurchaseManager.CanMakePayments)
                {
                    Console.WriteLine("Xamarin.InAppBilling: User can make payments to iTunes App Store.");
                }
                else
                {
                    //Display Alert Dialog Box
                    using (var alert = new UIAlertView("Xamarin.InAppBilling", "Sorry but you cannot make purchases from the In App Billing store. Please try again later.", null, "OK", null))
                    {
                        alert.Show();
                    }

                }

                // Warn user if the Purchase Manager is unable to connect to
                // the network.
                PurchaseManager.NoInternetConnectionAvailable += () => {
                    //Display Alert Dialog Box
                    using (var alert = new UIAlertView("Xamarin.InAppBilling", "No open internet connection is available.", null, "OK", null))
                    {
                        alert.Show();
                    }
                };

                // Show any invalid product queries
                PurchaseManager.ReceivedInvalidProducts += (productIDs) => {
                    // Display any invalid product IDs to the console
                    Console.WriteLine("The following IDs were rejected by the iTunes App Store:");
                    foreach (string ID in productIDs)
                    {
                        Console.WriteLine(ID);
                    }
                    Console.WriteLine(" ");
                };

                // Report the results of the user restoring previous purchases
                PurchaseManager.InAppPurchasesRestored += (count) => {
                    // Anything restored?
                    if (count == 0)
                    {
                        // No, inform user
                        using (var alert = new UIAlertView("Xamarin.InAppPurchase", "No products were available to be restored from the iTunes App Store.", null, "OK", null))
                        {
                            alert.Show();
                        }
                    }
                    else
                    {
                        // Yes, inform user
                        using (var alert = new UIAlertView("Xamarin.InAppPurchase", String.Format("{0} {1} restored from the iTunes App Store.", count, (count > 1) ? "products were" : "product was"), null, "OK", null))
                        {
                            alert.Show();
                        }
                    }
                };

                // Report miscellanous processing errors
                PurchaseManager.InAppPurchaseProcessingError += (message) => {
                    //Display Alert Dialog Box
                    using (var alert = new UIAlertView("Xamarin.InAppPurchase", message, null, "OK", null))
                    {
                        alert.Show();
                    }
                };

                // Report any issues with persistence
                PurchaseManager.InAppProductPersistenceError += (message) => {
                    using (var alert = new UIAlertView("Xamarin.InAppPurchase", message, null, "OK", null))
                    {
                        alert.Show();
                    }
                };

                // Setup automatic purchase persistance and load any previous purchases
                PurchaseManager.AutomaticPersistenceType = InAppPurchasePersistenceType.LocalFile;
                PurchaseManager.PersistenceFilename = "AtomicData";
                PurchaseManager.ShuffleProductsOnPersistence = false;
                PurchaseManager.RestoreProducts();

#if SIMULATED
			// Ask the iTunes App Store to return information about available In App Products for sale
			PurchaseManager.QueryInventory (new string[] { 
				"product.nonconsumable",
				"feature.nonconsumable",
				"feature.nonconsumable.fail",
				"gold.coins.consumable_x25",
				"gold.coins.consumable_x50",
				"gold.coins.consumable_x100",
				"newsletter.freesubscription",
				"magazine.subscription.duration1month",
				"antivirus.nonrenewingsubscription.duration6months",
				"antivirus.nonrenewingsubscription.duration1year",
				"product.nonconsumable.invalid",
				"content.nonconsumable.downloadable",
				"content.nonconsumable.downloadfail",
				"content.nonconsumable.downloadupdate"
			});

			// Setup the list of simulated purchases to restore when doing a simulated restore of pruchases
			// from the iTunes App Store
			PurchaseManager.SimulatedRestoredPurchaseProducts = "product.nonconsumable,antivirus.nonrenewingsubscription.duration6months,content.nonconsumable.downloadable";
#else
                // Ask the iTunes App Store to return information about available In App Products for sale
                PurchaseManager.QueryInventory(new string[] {
                //"xam.iap.nonconsumable.widget",
                //"xam.iap.subscription.duration1month",
                //"xam.iap.subscription.duration1year",
                //"xam.iap.subscription.duration3months"
                "yomeedpremium_monthly",
                "yomeedpremium_yearly",
                "test_product"
            });
#endif
               
                  
                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController
                    (_ViewController1,
                  true, null);
            }
            catch (Exception ex)
            {

            }
        }


    }
}