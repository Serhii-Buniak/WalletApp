using WalletApp.BLL.Dtos.DailyPointDtos;

namespace WalletApp.BLL.Services.Interfaces;

public interface IDailyPointService
{
    DailyPointReadDto Get();
}