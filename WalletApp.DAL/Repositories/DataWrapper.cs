using WalletApp.DAL.Repositories.Interfaces;
using WalletApp.DAL.Repositories.Realizations;

namespace WalletApp.DAL.Repositories;

public class DataWrapper : IDataWrapper, IDisposable
{
    protected AppDbContext Context { get; set; }

    private ITransactionRepository? _transactionRepository;
    private ICardBalanceRepository? _cardBalanceRepository;
    private IUsersRepository? _usersRepository;
    private IPaymentDueRepository? _paymentDueRepository;
    private IDailyPointRepository? _dailyPointRepository;

    public DataWrapper(AppDbContext context)
    {
        Context = context;
    }

    public ITransactionRepository Transactions
    {
        get
        {
            _transactionRepository ??= new TransactionRepository(Context);
            return _transactionRepository;
        }
    }   
    
    public ICardBalanceRepository CardBalances
    {
        get
        {
            _cardBalanceRepository ??= new CardBalanceRepository(Context);
            return _cardBalanceRepository;
        }
    }

    public IUsersRepository Users
    {
        get
        {
            _usersRepository ??= new UsersRepository(Context);
            return _usersRepository;
        }
    }

    public IPaymentDueRepository PaymentDues
    {
        get
        {
            _paymentDueRepository ??= new PaymentDueRepository(Context);
            return _paymentDueRepository;
        }
    }

    public IDailyPointRepository DailyPoints
    {
        get
        {
            _dailyPointRepository ??= new DailyPointRepository(Context);
            return _dailyPointRepository;
        }
    }

    public int Save()
    {
        return Context.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await Context.SaveChangesAsync();
        }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }

        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
