using WalletApp.BLL.Dtos.UserDtos;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.Tests.TestHelpers;

public static class UserTestHelper
{
    public static IEnumerable<UserReadDto> GetUserReadDtos()
    {
        return new List<UserReadDto>()
        {
            new()
            {
                Id = new("111f39e2-9364-41a8-8c49-9a6c89d657cb"),
                UserName = "Diana",

            },
            new()
            {
                Id = new("222f39e2-9364-41a8-8c49-9a6c89d657cb"),
                UserName = "Serhii",
            },
        };
    }

    public static UserReadDto GetUserReadDto()
    {
        return GetUserReadDtos().First();
    }

    public static AppUser GetUser()
    {
        UserReadDto userReadDto = GetUserReadDto();
        return new AppUser
        {
            Id = userReadDto.Id,
            UserName = userReadDto.UserName
        };
    }
}
