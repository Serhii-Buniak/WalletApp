using WalletApp.DAL.Entities;

namespace WalletApp.DAL.Repositories.Interfaces;

public interface IPaymentDueRepository
{
    Task<PaymentDue?> GetByIdOrDefaultAsync(long id);
    Task<IEnumerable<PaymentDue>> GetAllAsync();
}
