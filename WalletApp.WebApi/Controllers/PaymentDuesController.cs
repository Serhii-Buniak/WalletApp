using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentDuesController : ControllerBase
{
    private readonly IPaymentDueService _paymentDueSrv;

    public PaymentDuesController(IPaymentDueService paymentDueSrv)
    {
        _paymentDueSrv = paymentDueSrv;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        try
        {
            PaymentDueReadDto paymentDueReadDto = await _paymentDueSrv.GetByIdAsync(id);
            return Ok(paymentDueReadDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }
    }
}
