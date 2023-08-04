using WalletApp.BLL.Dtos.PaymentDueDtos;

namespace WalletApp.BLL.Services.Interfaces;

public interface IPaymentDueService
{
    Task<PaymentDueReadDto> GetByIdAsync(long id);
}
