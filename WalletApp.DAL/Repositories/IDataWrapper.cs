using WalletApp.DAL.Repositories.Interfaces;
using WalletApp.DAL.Repositories.Realizations;

namespace WalletApp.DAL.Repositories;

public interface IDataWrapper
{
    ITransactionRepository Transactions { get; }
    ICardBalanceRepository CardBalances { get; }
    IUsersRepository Users { get; }
    IPaymentDueRepository PaymentDues { get; }
    IDailyPointRepository DailyPoints { get; }

    int Save();
    Task<int> SaveAsync();
}