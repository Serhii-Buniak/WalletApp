using WalletApp.BLL.Dtos.DailyPointDtos;

namespace WalletApp.BLL.Services.Interfaces;

public interface IDailyPointService
{
    Task<DailyPointReadDto> GetByIdAsync(long id);
}