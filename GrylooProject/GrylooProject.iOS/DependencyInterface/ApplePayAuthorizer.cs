using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using GrylooProject.iOS.DependencyInterface;
using GrylooProject.Repository;
using PassKit;
using Rg.Plugins.Popup.Extensions;

using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ApplePayAuthorizer))]

namespace GrylooProject.iOS.DependencyInterface
{
    public class ApplePayAuthorizer :
    PKPaymentAuthorizationViewControllerDelegate, Interface.IApplePayAuthorizer

    {
        void HandleAction()
        {

        }

        public ApplePayAuthorizer()
        {
        }


        void ShowAuthorizationAlert()
        {
            var alert = UIAlertController.Create("Error", "This device cannot make payments.", UIAlertControllerStyle.Alert);
            var action = UIAlertAction.Create("Okay", UIAlertActionStyle.Default, null);
            alert.AddAction(action);

            //await App.Current.MainPage.DisplayAlert("Snapco","This device cannot make payments.", "OK");

        }
        readonly NSString[] supportedNetworks = {
           PKPaymentNetwork.Amex,
            PKPaymentNetwork.Discover,
            PKPaymentNetwork.MasterCard,
            PKPaymentNetwork.Visa
        };

        public bool AuthorizePayment(string Amount)
        {
            

            if (!PKPaymentAuthorizationViewController.CanMakePayments)
            {
                ShowAuthorizationAlert();

            }

            if (!PKPaymentAuthorizationViewController.CanMakePaymentsUsingNetworks(supportedNetworks))
            {
                ShowAuthorizationAlert();



            }

            NSString[] paymentNetworks = new NSString[] { PKPaymentNetwork.Visa,PKPaymentNetwork.Discover,

                PKPaymentNetwork.MasterCard, PKPaymentNetwork.Amex};
            var merchantID = "merchant.com.orem.grylloo";
            // Enter merchant ID registered in apple.developer.com

            PKPaymentRequest paymentRequest = new PKPaymentRequest();
            paymentRequest.MerchantIdentifier = merchantID;
            paymentRequest.SupportedNetworks = paymentNetworks;
            paymentRequest.MerchantCapabilities = PKMerchantCapability.ThreeDS;
            paymentRequest.CountryCode = "US";
            paymentRequest.CurrencyCode = "USD";

            paymentRequest.PaymentSummaryItems = new PKPaymentSummaryItem[]{
                   new PKPaymentSummaryItem(){
                      Label = "Payable Amount" ,
                    Amount = new NSDecimalNumber(Amount)
                   }
                };
            Task.Delay(10000);
            PKPaymentAuthorizationViewController controller = new
                    PKPaymentAuthorizationViewController(paymentRequest);
            controller.Delegate = (PassKit.IPKPaymentAuthorizationViewControllerDelegate)Self;
            var rootController = UIApplication.SharedApplication.
                  KeyWindow.RootViewController;
            rootController.PresentViewController(controller,
                   true, null);


             Task.Delay(50000);

            return false;

        }

        public override void DidAuthorizePayment(PKPaymentAuthorizationViewController controller, PKPayment payment,
               Action<PKPaymentAuthorizationStatus> completion)
        {
            completion(PKPaymentAuthorizationStatus.Success);
            // Checkout();
            PremimumUser();
        }



        public override void PaymentAuthorizationViewControllerDidFinish
              (PKPaymentAuthorizationViewController controller)
        {
            controller.DismissViewController(true, null);

        }

        public override void WillAuthorizePayment(PKPaymentAuthorizationViewController controller)
        {
            //throw new NotImplementedException();
        }


      

        public async void PremimumUser()
        {

            string aa = Model.LoginDetails.userId;
            if (aa != null)
            {
                try
                {

                    string postData = "UserId=" + Model.LoginDetails.userId;

                    var result = await CommonLib.UpdateToken(CommonLib.ws_MainUrlMain + "UserApi/MakePremimum?" + postData);


                   if(result.Status!=0){
                       
                     //  await App.Current.MainPage.DisplayAlert("Grylloo", "Payment successful", "OK");


                       // await  App.Current.MainPage.Navigation.PopAsync();

                       
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }
    }
}
