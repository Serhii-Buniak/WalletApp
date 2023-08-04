using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Tests.Helpers;
using WalletApp.WebApi.Controllers;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Tests.Controllers;

internal class PaymentDuesControllerTests
{
    private Mock<IPaymentDueService> _paymentDueSrv;
    private PaymentDuesController _controller;

    [SetUp]
    public void Setup()
    {
        _paymentDueSrv = new Mock<IPaymentDueService>();
        _controller = new PaymentDuesController(_paymentDueSrv.Object);
    }

    [Test]
    public async Task GetById_Success_ReturnOkObjectResult()
    {
        _paymentDueSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(PaymentDueTestHelper.GetPaymentDueReadDto());

        IActionResult result = await _controller.GetById(It.IsAny<long>());

        var okObjectResult = result as OkObjectResult;

        var paymentDueReadDto = okObjectResult?.Value as PaymentDueReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(paymentDueReadDto?.Id, Is.EqualTo(CardBalanceTestHelper.GetCardBalanceReadDto().Id));
        });
    }

    [Test]
    public async Task GetById_NotFoundException_ReturnNotFoundObjectResult()
    {
        _paymentDueSrv
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
