using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq.Expressions;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Common;

namespace WalletApp.DAL.Repositories;

public abstract class GenericRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    protected AppDbContext Context { get; set; }

    protected GenericRepository(AppDbContext appDbContext)
    {
        Context = appDbContext;
    }

    public virtual async Task<TEntity?> GetByIdOrDefaultAsync(TId id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetQuery(include).FirstOrDefaultAsync(ent => ent.Id.Equals(id));
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await GetQuery().ToListAsync();
    }

    private IQueryable<TEntity> GetQuery(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = Context.Set<TEntity>().AsNoTracking();

        if (include != null)
        {
            query = include(query);
        }

        return query;
    }
}
