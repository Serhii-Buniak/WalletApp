using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.AuthDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LogInDto logInDto)
    {
        try
        {
            AuthTokenDto token = await _authService.LogInAsync(logInDto);
            return Ok(token);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }
        catch (AuthException)
        {
            return BadRequest(ErrorResponse.Create("Password uncorrect"));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        try
        {
            await _authService.RegisterAsync(registerDto);
            return Ok();
        }
        catch (ValidationModelException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
}
