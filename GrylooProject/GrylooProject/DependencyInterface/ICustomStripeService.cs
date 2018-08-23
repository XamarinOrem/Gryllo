using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrylooProject.DependencyInterface
{
    public class StripeCardInfo
    {
        public string CardNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string CCV { get; set; }
    }
    public class InforAutosStripeCharge
    {
        public string FailureMessage { get; set; }
        public bool IsPaid { get; set; }
        public string ID { get; set; }

    }
    public class AppPurchase
    {
        public bool IsPaid { get; set; }

    }
    public interface ICustomStripeService
    {
        Task<InforAutosStripeCharge> MakePayment(StripeCardInfo cardInfo, double Amount);
        void MakePayment1(StripeCardInfo cardInfo, double Amount);
    }



   
}
