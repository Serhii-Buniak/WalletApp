using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Common;
using WalletApp.DAL.Entities.Identity;
using WalletApp.DAL.Repositories.Interfaces;

namespace WalletApp.DAL.Repositories.Realizations;

public class UsersRepository : GenericRepository<AppUser, Guid>, IUsersRepository
{
    public UsersRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

}
