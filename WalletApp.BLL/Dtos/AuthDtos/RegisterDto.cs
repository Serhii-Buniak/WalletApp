using WalletApp.Common.Mapping;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.BLL.Dtos.AuthDtos;

public class RegisterDto : IMapTo<AppUser>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
