using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.DAL.Entities;
using WalletApp.Tests.TestHelpers;

namespace WalletApp.Tests.Helpers;

public static class CardBalanceTestHelper
{
    public static CardBalanceReadDto GetCardBalanceReadDto()
    {
        return new CardBalanceReadDto
        {
            Id = 1,
            Sum = 100,
            Available = 100
        };
    }  
    
    public static CardBalance GetCardBalance()
    {
        return new CardBalance
        {
            Id = 1,
            Sum = 100,
            User = UserTestHelper.GetUser()
        };
    }
}
