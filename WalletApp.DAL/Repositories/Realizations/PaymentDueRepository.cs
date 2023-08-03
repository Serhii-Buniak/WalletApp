using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories.Interfaces;

namespace WalletApp.DAL.Repositories.Realizations;

public class PaymentDueRepository : GenericRepository<PaymentDue, long>, IPaymentDueRepository
{
    public PaymentDueRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<PaymentDue?> GetByIdOrDefaultAsync(long id)
    {
        return await base.GetByIdOrDefaultAsync(id);
    }
}
