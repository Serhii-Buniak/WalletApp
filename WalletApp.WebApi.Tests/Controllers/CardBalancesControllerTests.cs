using Microsoft.AspNetCore.Mvc;
using Moq;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Tests.Helpers;
using WalletApp.Tests.TestHelpers;
using WalletApp.WebApi.Controllers;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Tests.Controllers;

internal class CardBalancesControllerTests
{
    private Mock<ICardBalanceService> _cardBalanceSrv;
    private CardBalancesController _controller;

    [SetUp]
    public void Setup()
    {
        _cardBalanceSrv = new Mock<ICardBalanceService>();
        _controller = new CardBalancesController(_cardBalanceSrv.Object);
    }

    [Test]
    public async Task GetById_Success_ReturnOkObjectResult()
    {
        _cardBalanceSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(CardBalanceTestHelper.GetCardBalanceReadDto());

        IActionResult result = await _controller.GetById(It.IsAny<long>());

        var okObjectResult = result as OkObjectResult;

        var cardBalanceReadDto = okObjectResult?.Value as CardBalanceReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(cardBalanceReadDto?.Id, Is.EqualTo(CardBalanceTestHelper.GetCardBalanceReadDto().Id));
        });
    }

    [Test]
    public async Task GetById_NotFoundException_ReturnNotFoundObjectResult()
    {
        _cardBalanceSrv
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
