using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Pagination;

namespace WalletApp.BLL.Services.Realizations;

public class TransactionService : ITransactionService
{
    public Task<TransactionReadDto> AddAsync(TransactionAddDto transactionAdd)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TransactionReadDto>> GetAllAsync(PageParameters pageParameters)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionReadDto> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
