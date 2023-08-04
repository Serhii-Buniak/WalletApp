using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.DAL.Entities;
using WalletApp.Tests.TestHelpers;

namespace WalletApp.Tests.Helpers;

public static class DailyPointTestHelper
{
    public static DailyPointReadDto GetDailyPointReadDto()
    {
        return new DailyPointReadDto
        {
            Id = 1,
            Count = 2600,
            DisplayCount = "3K"
        };
    }   
    
    public static DailyPoint GetDailyPoint()
    {
        return new DailyPoint()
        {
            Id = 1,
            User = UserTestHelper.GetUser(),
            Count = 2600,
        };
    }
}
