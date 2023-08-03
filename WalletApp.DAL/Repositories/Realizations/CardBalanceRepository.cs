using Microsoft.EntityFrameworkCore.Query;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories.Interfaces;

namespace WalletApp.DAL.Repositories.Realizations;

public class CardBalanceRepository : GenericRepository<CardBalance, long>, ICardBalanceRepository
{
    public CardBalanceRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<CardBalance?> GetByIdOrDefaultAsync(long id)
    {
        return await base.GetByIdOrDefaultAsync(id);
    }
}

