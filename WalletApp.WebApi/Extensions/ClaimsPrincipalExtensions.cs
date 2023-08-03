using System.Security.Claims;
using WalletApp.Common.Exceptions;

namespace WalletApp.WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool IsAuthenticated(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Identity?.IsAuthenticated ?? false;
    }    
    
    public static Guid? GetId(this ClaimsPrincipal principal)
    {
        string? stringId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (stringId == null)
        {
            return null;
        }

        return Guid.Parse(stringId);
    }      
    
    public static bool TryGetId(this ClaimsPrincipal principal, out Guid id)
    {
        if (!IsAuthenticated(principal))
        {
            id = Guid.Empty;
            return false;
        }

        Guid? idOrNull = GetId(principal);

        if (!idOrNull.HasValue)
        {
            throw new InvalidClaimsPrincipal("Authenticated user don't have NameIdentifier");
        }

        id = idOrNull.Value;
        return true;
    }    
   
}
