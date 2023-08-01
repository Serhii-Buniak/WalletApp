using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.WebApi.Requests;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        IEnumerable<TransactionReadDto> transactions = await _transactionService.GetAllAsync();
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
            return NotFound(NotFoundResponse.Create(ex));
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] TransactionAddRequest request)
    {
        IEnumerable<TransactionReadDto> transactions = await _transactionService.GetAllAsync();
        return Ok(transactions);
    }
}
