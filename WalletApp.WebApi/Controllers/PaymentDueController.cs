using Microsoft.AspNetCore.Mvc;

namespace WalletApp.WebApi.Controllers;

public class PaymentDueController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    { 
        return Ok();
    }
}
