﻿using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Dtos.PaymentDueDtos;
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<DailyPointReadDto> dailyPointReadDtos = await _dailyPointSrv.GetAllAsync();
        return Ok(dailyPointReadDtos);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        try
        {
            DailyPointReadDto dailyPointReadDto = await _dailyPointSrv.GetByIdAsync(id);
            return Ok(dailyPointReadDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ErrorResponse.Create(ex));
        }
    }
}