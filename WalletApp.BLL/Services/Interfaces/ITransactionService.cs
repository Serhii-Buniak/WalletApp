using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.Common.Pagination;

namespace WalletApp.BLL.Services.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionReadDto>> GetAllAsync(PageParameters pageParameters);
    Task<TransactionReadDto> GetByIdAsync(long id);
    Task<TransactionReadDto> AddAsync(TransactionAddDto transactionAdd);
}
