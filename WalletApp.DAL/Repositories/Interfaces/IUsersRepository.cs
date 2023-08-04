using Microsoft.EntityFrameworkCore.Query;
using System.Security.Cryptography;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.DAL.Repositories.Interfaces;

public interface IUsersRepository
{
    Task<AppUser?> GetByIdOrDefaultAsync(Guid id, Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>>? include = null);
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task<bool> ContainsByIdAsync(Guid id);
}
