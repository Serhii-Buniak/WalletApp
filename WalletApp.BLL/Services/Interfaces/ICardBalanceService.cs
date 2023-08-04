using WalletApp.BLL.Dtos.CardBalanceDtos;

namespace WalletApp.BLL.Services.Interfaces;

public interface ICardBalanceService
{
    Task<CardBalanceReadDto> GetByIdAsync(long id);
}
