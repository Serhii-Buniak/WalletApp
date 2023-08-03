﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Common.Pagination;
using WalletApp.Tests.TestHelpers;
using WalletApp.WebApi.Controllers;
using WalletApp.WebApi.Requests;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Tests.Controllers;

internal class TransactionControllerTests
{
    private Mock<ITransactionService> _transactionSrv;
    private Mock<IMapper> _mapper;
    private TransactionsController _controller;

    [SetUp]
    public void Setup()
    {
        _transactionSrv = new Mock<ITransactionService>();
        _mapper = new Mock<IMapper>();

        _controller = new TransactionsController(_transactionSrv.Object, _mapper.Object);
    }

    [Test]
    public async Task GetAllAsync_Success_ReturnOkObjectResult()
    {
        _transactionSrv
            .Setup(t => t.GetAllAsync(It.IsAny<PageParameters>()))
            .ReturnsAsync(TransactionTestHelper.GetTransactionReadDtos());

        IActionResult result = await _controller.GetAllAsync(It.IsAny<PageParameters>());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDtos = okObjectResult?.Value as IEnumerable<TransactionReadDto>;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(transactionReadDtos?.Count(), Is.EqualTo(TransactionTestHelper.GetTransactionReadDtos().Count()));
        });
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

    [Test]
    public async Task AddAsync_Success_ReturnOkObjectResult()
    {
        _mapper
            .Setup(t => t.Map<TransactionAddDto>(It.IsAny<TransactionAddRequest>()))
            .Returns(TransactionTestHelper.GetTransactionAddDto());

        _transactionSrv
            .Setup(t => t.AddAsync(It.IsAny<TransactionAddDto>()))
            .ReturnsAsync(TransactionTestHelper.GetTransactionReadDto());

        _controller.ControllerContext = new ControllerContextBuilder().Build();

        IActionResult result = await _controller.AddAsync(It.IsAny<TransactionAddRequest>());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDto = okObjectResult?.Value as TransactionReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(transactionReadDto?.Id, Is.EqualTo(TransactionTestHelper.GetTransactionReadDto().Id));
        });
    }

    [Test]
    public async Task AddAsync_SuccessIsAuthenticated_ReturnUserNameIsNotNull()
    {
        TransactionAddDto transactionAddDto = new();
        _mapper
            .Setup(t => t.Map<TransactionAddDto>(It.IsAny<TransactionAddRequest>()))
            .Returns(transactionAddDto);

        _transactionSrv
            .Setup(t => t.AddAsync(It.IsAny<TransactionAddDto>()))
            .ReturnsAsync(() => TransactionTestHelper.GetTransactionReadDtoFromAdd(transactionAddDto));

        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims().Build();

        IActionResult result = await _controller.AddAsync(It.IsAny<TransactionAddRequest>());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDto = okObjectResult?.Value as TransactionReadDto;

        var username = transactionReadDto?.SenderName;

        Assert.That(username, Is.Not.Null);
    }    
    
    [Test]
    public async Task AddAsync_SuccessIsNotAuthenticated_ReturnUserNameIsNull()
    {
        TransactionAddDto transactionAddDto = new();
        _mapper
            .Setup(t => t.Map<TransactionAddDto>(It.IsAny<TransactionAddRequest>()))
            .Returns(transactionAddDto);

        _transactionSrv
            .Setup(t => t.AddAsync(It.IsAny<TransactionAddDto>()))
            .ReturnsAsync(() => TransactionTestHelper.GetTransactionReadDtoFromAdd(transactionAddDto));

        _controller.ControllerContext = new ControllerContextBuilder().Build();

        IActionResult result = await _controller.AddAsync(It.IsAny<TransactionAddRequest>());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDto = okObjectResult?.Value as TransactionReadDto;

        var username = transactionReadDto?.SenderName;

        Assert.That(username, Is.Null);
    }
}
