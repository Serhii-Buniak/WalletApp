using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WalletApp.Common.Pagination;
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

    public virtual async Task<bool> ContainsByIdAsync(TId id)
    {
        return await (GetByIdOrDefaultAsync(id)) != null;
    }    
    
    public virtual async Task<TEntity?> GetByIdOrDefaultAsync(TId id, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetQuery(predicate, include).FirstOrDefaultAsync(ent => ent.Id.Equals(id));
    }

    public async Task<EntityEntry<TEntity>> CreateAsync(TEntity entity)
    {
        return await Context.Set<TEntity>().AddAsync(entity);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetQuery(predicate, include).ToListAsync();
    }

    public virtual async Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = Context.Set<TEntity>().AsNoTracking();

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        int pageNumber = pageParameters.PageNumber;
        int pageSize = pageParameters.PageSize;

        int count = await query.CountAsync();


        query = query.Skip((pageSize * (pageNumber - 1)))
            .Take(pageSize);


        var entities = await query.ToListAsync();

        return new PagedList<TEntity>(entities, count, pageParameters);
    }

    private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = Context.Set<TEntity>().AsNoTracking();

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return query;
    }
}
