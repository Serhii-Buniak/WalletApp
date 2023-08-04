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

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
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
}
