using WalletApp.BLL.Dtos.UserDtos;

namespace WalletApp.WebApi.Tests.TestHelpers;

internal static class UserTestHelper
{
    public static IEnumerable<UserReadDto> GetUserReadDtos()
    {
        return new List<UserReadDto>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Apple",

            },     
            new()
            {
                Id = Guid.NewGuid(),
                Name = "IKEA",
            },
        };
    }    
    
    public static UserReadDto GetUserReadDto()
    {
        return GetUserReadDtos().First();
    }
}
