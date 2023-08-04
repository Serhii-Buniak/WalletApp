using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Dtos.UserDtos;
using WalletApp.Common.Enums;
using WalletApp.Common.Pagination;

namespace WalletApp.Tests.TestHelpers;

public static class TransactionTestHelper
{
    public static TransactionReadDto GetTransactionReadDtoFromAdd(TransactionAddDto transactionAddDto)
    {
        var userName = UserTestHelper.GetUserReadDtos().FirstOrDefault(u => u.Id == transactionAddDto.SenderId)?.UserName;
        return new TransactionReadDto()
        {
            SenderName = userName
        };
    }    
    public static TransactionReadDto GetTransactionReadDto()
    {
        return GetTransactionReadDtos().First();
    }

    public static TransactionAddDto GetTransactionAddDto()
    {
        return new TransactionAddDto
        {

        };
    }
    
    public static PagedList<TransactionReadDto> GetTransactionReadDtoPage()
    {
        return new PagedList<TransactionReadDto>(GetTransactionReadDtos().ToList(), 3, new PageParameters());
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
                SenderName = UserTestHelper.GetUserReadDto().UserName,
            },
            new()
            {
                Id = 2,
                Name = "IKEA",
                Description = "This is 2 Transaction from IKEA",
                IsPending = true,
                Type = TransactionType.Payment,
                WasCreated = "Tuesday",
                SenderName = UserTestHelper.GetUserReadDto().UserName,
            },
            new()
            {
                Id = 3,
                Name = "Target",
                Description = "This is 3 Transaction from Target",
                IsPending = false,
                Type = TransactionType.Credit,
                WasCreated = "Saturday",
                SenderName = UserTestHelper.GetUserReadDto().UserName,
            },
        };
    }
}
