using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Dtos.UserDtos;
using WalletApp.Common.Enums;

namespace WalletApp.WebApi.Tests.TestHelpers;

internal static class TransactionTestHelper
{
    public static TransactionReadDto GetTransactionReadDto()
    {
        return GetTransactionReadDtos().First();
    }

    public static IEnumerable<TransactionReadDto> GetTransactionReadDtos()
    {
        return new List<TransactionReadDto>()
        {
            new()
            {
                Id = 1,
                Name = "Apple",
                Description = "This is 1 Transaction from Apple",
                IsPending = true,
                Type = TransactionType.Payment,
                WasCreated = "Yesterday",
                IconName = ImageTestHelper.GetImageReadDto().Name,
                UserName = UserTestHelper.GetUserReadDto().Name,
            },
            new()
            {
                Id = 2,
                Name = "IKEA",
                Description = "This is 2 Transaction from IKEA",
                IsPending = true,
                Type = TransactionType.Payment,
                WasCreated = "Tuesday",
                IconName = ImageTestHelper.GetImageReadDto().Name,
                UserName = UserTestHelper.GetUserReadDto().Name,
            },
            new()
            {
                Id = 3,
                Name = "Target",
                Description = "This is 3 Transaction from Target",
                IsPending = false,
                Type = TransactionType.Credit,
                WasCreated = "Saturday",
                IconName = ImageTestHelper.GetImageReadDto().Name,
                UserName = UserTestHelper.GetUserReadDto().Name,
            },
        };
    }
}
