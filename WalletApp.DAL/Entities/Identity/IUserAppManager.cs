using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WalletApp.DAL.Entities.Identity;

public interface IUserAppManager
{
    Task<bool> CheckPasswordAsync(AppUser user, string password);
    Task<IdentityResult> CreateAsync(AppUser user, string password);
    Task<AppUser?> FindByNameAsync(string userName);
    Task<IList<Claim>> GetClaimsAsync(AppUser user);
    Task<IList<string>> GetRolesAsync(AppUser user);
}
