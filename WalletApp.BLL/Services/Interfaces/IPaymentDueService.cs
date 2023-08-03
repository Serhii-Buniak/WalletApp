using WalletApp.BLL.Dtos.PaymentDueDtos;

namespace WalletApp.BLL.Services.Interfaces;

public interface IPaymentDueService
{
    Task<IEnumerable<PaymentDueReadDto>> GetAllAsync();
    Task<PaymentDueReadDto> GetByIdAsync(long id);
}
