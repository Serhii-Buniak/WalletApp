using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Common.Pagination;
using WalletApp.WebApi.Extensions;
using WalletApp.WebApi.Requests;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;

    public TransactionsController(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageParameters pageParameters)
    {
        IEnumerable<TransactionReadDto> transactions = await _transactionService.GetAllAsync(pageParameters);
        return Ok(transactions);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetOneAsync(long id)
    {
        try
        {
            TransactionReadDto transaction = await _transactionService.GetByIdAsync(id);
            return Ok(transaction);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] TransactionAddRequest request)
    {
        var addDto = _mapper.Map<TransactionAddDto>(request);

        if (User.TryGetId(out Guid id))
        {
            addDto.SenderId = id;
        }

        TransactionReadDto transactions = await _transactionService.AddAsync(addDto);

        return Ok(transactions);
    }
}
