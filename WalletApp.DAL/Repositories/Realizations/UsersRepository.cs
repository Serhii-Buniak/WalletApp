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

    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        return await base.GetAllAsync();
    }

    public async Task<AppUser?> GetByIdOrDefaultAsync(Guid id, Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>>? include = null)
    {
       return await base.GetByIdOrDefaultAsync(id, include: include);
    }
}
