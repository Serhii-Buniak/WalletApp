using Microsoft.AspNetCore.Identity;

namespace WalletApp.DAL.Entities.Identity;

public class AppRole : IdentityRole<Guid>
{
    public AppRole()
    {

    }

    public AppRole(string name) : base(name)
    {

    }
}
