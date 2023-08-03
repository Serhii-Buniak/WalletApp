using WalletApp.BLL.Dtos.DailyPointDtos;

namespace WalletApp.BLL.Services.Interfaces;

public interface IDailyPointService
{
    Task<IEnumerable<DailyPointReadDto>> GetAllAsync();
    Task<DailyPointReadDto> GetByIdAsync(long id);
}