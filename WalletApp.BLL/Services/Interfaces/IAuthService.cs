using WalletApp.BLL.Dtos.AuthDtos;

namespace WalletApp.BLL.Services.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto registerDto);
    Task<AuthTokenDto> LogInAsync(LogInDto logInDto);
}
