using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Dtos.UserDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Common.Pagination;
using WalletApp.WebApi.Extensions;
using WalletApp.WebApi.Requests;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userSrv;
    private readonly ITransactionService _transactionSrv;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, ITransactionService transactionService, IMapper mapper)
    {
        _userSrv = userService;
        _transactionSrv = transactionService;
        _mapper = mapper;
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

    [HttpGet("{id:guid}/Transactions")]
    public async Task<IActionResult> GetUserTransactions(Guid id, [FromQuery] PageParameters pageParameters)
    {
        return await ActionGetTransactions(id, pageParameters);
    }


    [HttpPost("{id:guid}/Transactions")]
    public async Task<IActionResult> AddTransaction(Guid id, TransactionAddRequest request)
    {
        try
        {
            var addDto = _mapper.Map<TransactionAddDto>(request);

            if (User.TryGetId(out Guid authUserId))
            {
                addDto.SenderId = authUserId;
            }

            addDto.UserId = id;

            TransactionReadDto transactionReadDto = await _transactionSrv.AddAsync(addDto);

            return Ok(transactionReadDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }   
        catch (TransactionException ex)
        {
            return BadRequest(ErrorResponse.Create(ex.Message));
        }
  
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
    [Authorize]
    public async Task<IActionResult> GetMeDailyPoint()
    {
        Guid? id = User.GetId();

        if (!id.HasValue)
        {
            return BadRequest(ErrorResponse.Create("Access token is invalid"));
        }

        return await ActionGetUserDailyPoint(id.Value);
    }

    [HttpGet("Me/Transactions")]
    [Authorize]
    public async Task<IActionResult> GetMeTransactions( [FromQuery] PageParameters pageParameters)
    {
        Guid? id = User.GetId();

        if (!id.HasValue)
        {
            return BadRequest(ErrorResponse.Create("Access token is invalid"));
        }

        return await ActionGetTransactions(id.Value, pageParameters);
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
    private async Task<IActionResult> ActionGetTransactions(Guid id, PageParameters pageParameters)
    {
        try
        {
            PagedList<TransactionReadDto> transactionReadDtos = await _userSrv.GetTransactionReadDtosPageAsync(id, pageParameters);
            return Ok(transactionReadDtos);

        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }
    }

}
