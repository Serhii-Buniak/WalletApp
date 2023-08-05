using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DailyPointsController : ControllerBase
{
    private readonly IDailyPointService _dailyPointSrv;

    public DailyPointsController(IDailyPointService dailyPointSrv)
    {
        _dailyPointSrv = dailyPointSrv;
    }

    [HttpGet("Calculeted")]
    public IActionResult Get()
    {
        return Ok(_dailyPointSrv.Get());
    }
}