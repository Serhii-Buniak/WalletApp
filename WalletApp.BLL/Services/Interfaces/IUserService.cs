using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Dtos.UserDtos;

namespace WalletApp.BLL.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserReadDto>> GetAllAsync();
    Task<UserReadDto> GetByIdAsync(Guid id);
    Task<CardBalanceReadDto> GetCardBalanceReadDtoAsync(Guid userId);
    Task<DailyPointReadDto> GetDailyPointReadDtoAsync(Guid userId);
    Task<PaymentDueReadDto> GetPaymentDueReadDtoAsync(Guid userId);
}  