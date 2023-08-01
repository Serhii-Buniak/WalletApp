using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.WebApi.Controllers;
using WalletApp.WebApi.Responses;
using WalletApp.WebApi.Tests.TestHelpers;

namespace WalletApp.WebApi.Tests.Controllers;

internal class TransactionControllerTests
{
    private Mock<ITransactionService> _transactionSrv;
    private TransactionController _controller;

    [SetUp]
    public void Setup()
    {
        _transactionSrv = new Mock<ITransactionService>();


        _controller = new TransactionController(_transactionSrv.Object);
    }

    [Test]
    public async Task GetAllAsync_Success_ReturnOkObjectResult()
    {
        _transactionSrv
            .Setup(t => t.GetAllAsync())
            .ReturnsAsync(TransactionTestHelper.GetTransactionReadDtos());

        IActionResult result = await _controller.GetAllAsync();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }    
    
    [Test]
    public async Task GetAllAsync_Success_ReturnTransactionReadDtos()
    {
        _transactionSrv
            .Setup(t => t.GetAllAsync())
            .ReturnsAsync(TransactionTestHelper.GetTransactionReadDtos());

        IActionResult result = await _controller.GetAllAsync();
       
        var okObjectResult = result as OkObjectResult;

        var transactionReadDtos = okObjectResult?.Value as IEnumerable<TransactionReadDto>;

        Assert.That(transactionReadDtos?.Count(), Is.EqualTo(TransactionTestHelper.GetTransactionReadDtos().Count()));
    }
    
    [Test]
    public async Task GetOneAsync_Success_ReturnOkObjectResult()
    {
        _transactionSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(TransactionTestHelper.GetTransactionReadDto());

        IActionResult result = await _controller.GetOneAsync(It.IsAny<long>());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }    
    
    [Test]
    public async Task GetOneAsync_Success_ReturnTransactionReadDto()
    {
        _transactionSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(TransactionTestHelper.GetTransactionReadDto());

        IActionResult result = await _controller.GetOneAsync(It.IsAny<long>());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDto = okObjectResult?.Value as TransactionReadDto;

        Assert.That(transactionReadDto?.Id, Is.EqualTo(TransactionTestHelper.GetTransactionReadDto().Id));
    }

    [Test]
    public async Task GetOneAsync_NotFound_ReturnNotFoundObjectResult()
    {
        _transactionSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<long>()))
            .ThrowsAsync(new NotFoundException("ex message"));

        IActionResult result = await _controller.GetOneAsync(It.IsAny<long>());

        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }   
    
    [Test]
    public async Task GetOneAsync_NotFound_ReturnExMessage()
    {
        _transactionSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<long>()))
            .ThrowsAsync(new NotFoundException("ex message"));

        IActionResult result = await _controller.GetOneAsync(It.IsAny<long>());

        var okObjectResult = result as NotFoundObjectResult;

        var notFoundResponse = okObjectResult?.Value as NotFoundResponse;


        Assert.That(notFoundResponse?.Error, Is.EqualTo("ex message"));
    }
}
