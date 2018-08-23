using System;
namespace GrylooProject.Interface
{
    public interface IApplePayAuthorizer
    {

        bool AuthorizePayment(string Amount);

    }
}
