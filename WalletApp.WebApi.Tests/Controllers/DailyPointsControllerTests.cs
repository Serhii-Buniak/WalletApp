using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Tests.Helpers;
using WalletApp.WebApi.Controllers;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Tests.Controllers;

internal class DailyPointsControllerTests
{
    private Mock<IDailyPointService> _dailyPointSrv;
    private DailyPointsController _controller;

    [SetUp]
    public void Setup()
    {
        _dailyPointSrv = new Mock<IDailyPointService>();
        _controller = new DailyPointsController(_dailyPointSrv.Object);
    }

    [Test]
    public async Task GetById_Success_ReturnOkObjectResult()
    {
        _dailyPointSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(DailyPointTestHelper.GetDailyPointReadDto());

        IActionResult result = await _controller.GetById(It.IsAny<long>());

        var okObjectResult = result as OkObjectResult;

        var dailyPointReadDto = okObjectResult?.Value as DailyPointReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(dailyPointReadDto?.Id, Is.EqualTo(DailyPointTestHelper.GetDailyPointReadDto().Id));
        });
    }

    [Test]
    public async Task GetById_NotFoundException_ReturnNotFoundObjectResult()
    {
        _dailyPointSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<long>()))
            .ThrowsAsync(new NotFoundException("ex message"));

        IActionResult result = await _controller.GetById(It.IsAny<long>());

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

}
