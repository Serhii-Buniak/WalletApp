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
            Count = 2600,
            DisplayCount = "3K"
        };
    }   
}
