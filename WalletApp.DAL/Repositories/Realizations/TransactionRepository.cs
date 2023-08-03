using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories.Interfaces;

namespace WalletApp.DAL.Repositories.Realizations;

public class TransactionRepository : GenericRepository<Transaction, long>, ITransactionRepository
{
    public TransactionRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
