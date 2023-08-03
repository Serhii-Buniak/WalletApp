using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Dtos.UserDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.WebApi.Extensions;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userSrv;

    public UsersController(IUserService userService)
    {
        _userSrv = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<UserReadDto> userReadDtos = await _userSrv.GetAllAsync();
        return Ok(userReadDtos);

    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return await ActionGetById(id);
    }

    [HttpGet("{id:guid}/CardBalance")]
    public async Task<IActionResult> GetUserCardBalance(Guid id)
    {
        return await ActionGetUserCardBalance(id);
    }

    [HttpGet("{id:guid}/PaymentDue")]
    public async Task<IActionResult> GetUserPaymentDue(Guid id)
    {
        return await ActionGetUserPaymentDue(id);
    }    
    
    [HttpGet("{id:guid}/DailyPoint")]
    public async Task<IActionResult> GetUserDailyPoint(Guid id)
    {
        return await ActionGetUserDailyPoint(id);
    }

    [HttpGet("Me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        Guid? id = User.GetId();

        if (!id.HasValue)
        {
            return BadRequest(ErrorResponse.Create("Access token is invalid"));
        }

        return await ActionGetById(id.Value);
    }

    [HttpGet("Me/CardBalance")]
    [Authorize]
    public async Task<IActionResult> GetMeCardBalance()
    {
        Guid? id = User.GetId();

        if (!id.HasValue)
        {
            return BadRequest(ErrorResponse.Create("Access token is invalid"));
        }

        return await ActionGetUserCardBalance(id.Value);
    }

    [HttpGet("Me/PaymentDue")]
    [Authorize]
    public async Task<IActionResult> GetMePaymentDue()
    {
        Guid? id = User.GetId();

        if (!id.HasValue)
        {
            return BadRequest(ErrorResponse.Create("Access token is invalid"));
        }

        return await ActionGetUserPaymentDue(id.Value);
    }

    [HttpGet("Me/DailyPoint")]
    public async Task<IActionResult> GetMeDailyPoint()
    {
        Guid? id = User.GetId();

        if (!id.HasValue)
        {
            return BadRequest(ErrorResponse.Create("Access token is invalid"));
        }

        return await ActionGetUserDailyPoint(id.Value);
    }

    private async Task<IActionResult> ActionGetUserDailyPoint(Guid id)
    {
        try
        {
            DailyPointReadDto dailyPointReadDto = await _userSrv.GetDailyPointReadDtoAsync(id);
            return Ok(dailyPointReadDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }
    }    

    private async Task<IActionResult> ActionGetUserPaymentDue(Guid id)
    {
        try
        {
            PaymentDueReadDto paymentDueReadDto = await _userSrv.GetPaymentDueReadDtoAsync(id);
            return Ok(paymentDueReadDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }
    }    
    
    private async Task<IActionResult> ActionGetUserCardBalance(Guid id)
    {
        try
        {
            CardBalanceReadDto cardBalanceRead = await _userSrv.GetCardBalanceReadDtoAsync(id);
            return Ok(cardBalanceRead);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }
    }

    private async Task<IActionResult> ActionGetById(Guid id)
    {
        try
        {
            UserReadDto userReadDto = await _userSrv.GetByIdAsync(id);
            return Ok(userReadDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }
    }
}
