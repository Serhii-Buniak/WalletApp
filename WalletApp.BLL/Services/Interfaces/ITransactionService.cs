using WalletApp.BLL.Dtos.TransactionDtos;

namespace WalletApp.BLL.Services.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionReadDto>> GetAllAsync();
    Task<TransactionReadDto> GetByIdAsync(long id);
}

public class TransactionService : ITransactionService
{
    public Task<IEnumerable<TransactionReadDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TransactionReadDto> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}