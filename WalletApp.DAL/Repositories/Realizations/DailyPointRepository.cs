using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories.Interfaces;

namespace WalletApp.DAL.Repositories.Realizations;

public class DailyPointRepository : GenericRepository<DailyPoint, long>, IDailyPointRepository
{
    public DailyPointRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<DailyPoint?> GetByIdOrDefaultAsync(long id)
    {
        return await base.GetByIdOrDefaultAsync(id);
    }
}

