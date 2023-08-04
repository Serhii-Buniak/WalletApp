using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Tests.TestHelpers;
using WalletApp.WebApi.Controllers;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Tests.Controllers;

internal class TransactionControllerTests
{
    private Mock<ITransactionService> _transactionSrv;
    private TransactionsController _controller;

    [SetUp]
    public void Setup()
    {
        _transactionSrv = new Mock<ITransactionService>();
        _controller = new TransactionsController(_transactionSrv.Object);
    }

    [Test]
    public async Task GetOneAsync_Success_ReturnOkObjectResult()
    {
        _transactionSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(TransactionTestHelper.GetTransactionReadDto());

        IActionResult result = await _controller.GetOneAsync(It.IsAny<long>());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDto = okObjectResult?.Value as TransactionReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(transactionReadDto?.Id, Is.EqualTo(TransactionTestHelper.GetTransactionReadDto().Id));
        });
    }

    [Test]
    public async Task GetOneAsync_NotFound_ReturnNotFoundObjectResult()
    {
        _transactionSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<long>()))
            .ThrowsAsync(new NotFoundException("ex message"));

        IActionResult result = await _controller.GetOneAsync(It.IsAny<long>());

        var notFoundObjectResult = result as NotFoundObjectResult;

        var notFoundResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(notFoundResponse?.Error, Is.EqualTo("ex message"));
        });
    }
}
