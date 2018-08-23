using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrylooProject.DependencyInterface
{
    public interface IPremiumMembership
    {
        Task<AppPurchase> MakePayment(string Key);
        void GetPremiumMembership();

    }
}
