using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.DAL.Entities;
using WalletApp.Tests.TestHelpers;

namespace WalletApp.Tests.Helpers;

public static class PaymentDueTestHelper
{
    public static PaymentDueReadDto GetPaymentDueReadDto()
    {
        return new PaymentDueReadDto
        {
            Id = 1,
            Month = "August"
        };
    }  
    
    public static PaymentDue GetPaymentDue()
    {
        return new PaymentDue
        {
            Id = 1,
            User = UserTestHelper.GetUser()
        };
    }
}
