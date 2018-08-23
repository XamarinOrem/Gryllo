using System;
using System.Threading.Tasks;
using GrylooProject.DependencyInterface;

namespace GrylooProject.iOS.DependencyInterface
{
    public class ApplePay_iOS : ICustomStripeService
    {
        async Task<InforAutosStripeCharge> ICustomStripeService.MakePayment(StripeCardInfo cardInfo, double Amount)
        {

            //var _token  = CreateToken(cardInfo.CardNumber, cardInfo.Month, cardInfo.Year, cardInfo.CCV);
            InforAutosStripeCharge result = new InforAutosStripeCharge();
            return result;


        }
        async void ICustomStripeService.MakePayment1(StripeCardInfo cardInfo, double Amount)
        {


        }
    }
}
