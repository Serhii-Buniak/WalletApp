using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WalletApp.Common.Pagination;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Common;

namespace WalletApp.DAL.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdOrDefaultAsync(long id, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>>? include = null);
    Task<Transaction> CreateAsync(Transaction transaction);
    Task<PagedList<Transaction>> GetPageAsync(PageParameters pageParameters, Expression<Func<Transaction, bool>>? predicate = null, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>>? include = null);
}
