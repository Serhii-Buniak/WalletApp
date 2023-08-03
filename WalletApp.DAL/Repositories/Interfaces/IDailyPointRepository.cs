using WalletApp.DAL.Entities;

namespace WalletApp.DAL.Repositories.Interfaces;

public interface IDailyPointRepository
{
    Task<DailyPoint?> GetByIdOrDefaultAsync(long id);
    Task<IEnumerable<DailyPoint>> GetAllAsync();
}
