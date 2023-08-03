using WalletApp.Common.Mapping;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.BLL.Dtos.UserDtos;

public class UserReadDto : IMapFrom<AppUser>
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
}
