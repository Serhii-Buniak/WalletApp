using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardBalancesController : ControllerBase
{
    private readonly ICardBalanceService _cardBalanceSrv;

    public CardBalancesController(ICardBalanceService cardBalanceService)
    {
        _cardBalanceSrv = cardBalanceService;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        try
        {
            CardBalanceReadDto cardBalanceRead = await _cardBalanceSrv.GetByIdAsync(id);
            return Ok(cardBalanceRead);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }
    }
}
