using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using WalletApp.Common.Enums;
using WalletApp.Common.Exceptions;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Common;
using WalletApp.DAL.Repositories.Interfaces;

namespace WalletApp.DAL.Repositories.Realizations;

public class TransactionRepository : GenericRepository<Transaction, long>, ITransactionRepository
{
    public TransactionRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public new async Task<Transaction> CreateAsync(Transaction transaction)
    {
        
        if (!transaction.IsPending)
        {
            CardBalance cardBalance = await Context.CardBalances.FirstAsync(c => c.UserId == transaction.UserId);
            decimal sum = cardBalance.Sum;

            switch (transaction.Type)
            {
                case TransactionType.Payment:
                    var newPaymentSum = sum + transaction.Sum;

                    if (newPaymentSum > CardBalance.Max)
                    {
                        throw new TransactionException("Сard account limit exceeded");
                    }

                    cardBalance.Sum = newPaymentSum;

                    Context.CardBalances.Update(cardBalance);
                    break;
                case TransactionType.Credit:
                    var newCreditSum = sum - transaction.Sum;

                    if (0 > newCreditSum)
                    {
                        throw new TransactionException("lack of funds in the account");
                    }

                    cardBalance.Sum = newCreditSum;

                    Context.CardBalances.Update(cardBalance);
                    break;
            }
        }

        EntityEntry<Transaction> entry = await base.CreateAsync(transaction);

        await entry.Reference(p => p.Sender).LoadAsync();

        return entry.Entity;
    }

    public async Task<Transaction?> GetByIdOrDefaultAsync(long id, Func<IQueryable<Transaction>, IIncludableQueryable<Transaction, object>>? include = null)
    {
        return await base.GetByIdOrDefaultAsync(id, include: include);
    }
}
