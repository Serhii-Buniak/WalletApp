using Microsoft.AspNetCore.Mvc;

namespace WalletApp.WebApi.Controllers;

public class CardBalanceController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    { 
        return Ok();
    }
}
