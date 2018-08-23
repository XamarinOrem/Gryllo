using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Threading.Tasks;
using Stripe;
using Xamarin.Forms;
using GrylooProject.iOS.DependencyInterface;
using GrylooProject.DependencyInterface;


[assembly: Dependency(typeof(CustomStripeService))]
namespace GrylooProject.iOS.DependencyInterface
{
    public class CustomStripeService : ICustomStripeService
    {

        string API_KEY = "sk_test_vWX0Oo7yNNI4iSeLbOoEan1x";
        async Task<InforAutosStripeCharge> ICustomStripeService.MakePayment(StripeCardInfo cardInfo, double Amount)
        {

            //var _token  = CreateToken(cardInfo.CardNumber, cardInfo.Month, cardInfo.Year, cardInfo.CCV);
            InforAutosStripeCharge result = new InforAutosStripeCharge();
            try
            {
                var myCharge = new StripeChargeCreateOptions();
                var myToken = new StripeTokenCreateOptions();
                myToken.Card = new StripeCreditCardOptions()
                {
                    Number = cardInfo.CardNumber,
                    ExpirationMonth = cardInfo.Month,
                    ExpirationYear = cardInfo.Year,
                    Cvc = cardInfo.CCV
                };
                // Stripe amount transformation from double with 2 decimals
               // Amount = 1;
                myCharge.Amount = Int32.Parse((Amount * Int32.Parse("100")).ToString());

                myCharge.Currency = "EUR";
                var chargeService = new StripeChargeService(API_KEY);
                var tokenService = new StripeTokenService(API_KEY);
                var token = tokenService.Create(myToken);
                myCharge.SourceTokenOrExistingSourceId = token.Id;
                StripeCharge stripeCharge = await chargeService.CreateAsync(myCharge);
                result.FailureMessage = stripeCharge.FailureMessage;
                result.IsPaid = stripeCharge.Paid;
                result.ID = stripeCharge.Id;
            }
            catch (System.Exception ex)
            {
                // result.FailureMessage = "Payment Successful";
                result.FailureMessage = ex.Message;
            }

            return result;
        }
        async void ICustomStripeService.MakePayment1(StripeCardInfo cardInfo, double Amount)
        {


        }

        public string CreateToken(string cardNumber, int cardExpMonth, int cardExpYear, string cardCVC)
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_70zfg67P7qQ12FiVLzET76sH");

                var tokenOptions = new StripeTokenCreateOptions()
                {
                    Card = new StripeCreditCardOptions()
                    {
                        Number = cardNumber,
                        ExpirationYear = cardExpYear,
                        ExpirationMonth = cardExpMonth,
                        Cvc = cardCVC
                    }
                };

                var tokenService = new StripeTokenService();
                StripeToken stripeToken = tokenService.Create(tokenOptions);

                return stripeToken.Id; // This is the token
            }
            catch (Exception ex)
            {
                return "error";
            }
        }
    }
}