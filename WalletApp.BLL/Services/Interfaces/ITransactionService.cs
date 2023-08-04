using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.Common.Pagination;

namespace WalletApp.BLL.Services.Interfaces;

public interface ITransactionService
{
    Task<TransactionReadDto> GetByIdAsync(long id);
    Task<TransactionReadDto> AddAsync(TransactionAddDto transactionAdd);
}
