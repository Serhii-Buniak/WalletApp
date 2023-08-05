using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Dtos.PaymentDueDtos;
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
    public void Get_Success_ReturnOkObjectResult()
    {
        _dailyPointSrv
            .Setup(t => t.Get())
            .Returns(DailyPointTestHelper.GetDailyPointReadDto());

        IActionResult result = _controller.Get();

        var okObjectResult = result as OkObjectResult;

        var dailyPointReadDto = okObjectResult?.Value as DailyPointReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(dailyPointReadDto, Is.EqualTo(DailyPointTestHelper.GetDailyPointReadDto()));
        });
    }
}
