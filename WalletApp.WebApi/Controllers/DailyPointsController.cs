using Microsoft.AspNetCore.Mvc;

namespace WalletApp.WebApi.Controllers;

public class DailyPointsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    { 
        return Ok();
    }
}