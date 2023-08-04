using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Dtos.UserDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Common.Pagination;
using WalletApp.Tests.Helpers;
using WalletApp.Tests.TestHelpers;
using WalletApp.WebApi.Controllers;
using WalletApp.WebApi.Requests;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Tests.Controllers;

internal class UsersControllerTests
{
    private UsersController _controller;
    private Mock<IUserService> _userSrv;
    private Mock<ITransactionService> _transactionSrv;
    private Mock<IMapper> _mapper;

    [SetUp]
    public void Setup()
    {

        _userSrv = new Mock<IUserService>();
        _transactionSrv = new Mock<ITransactionService>();
        _mapper = new Mock<IMapper>();

        _controller = new UsersController(_userSrv.Object, _transactionSrv.Object, _mapper.Object);
    }

    [Test]
    public async Task GetAll_Success_ReturnOkObjectResult()
    {
        _userSrv.Setup(u => u.GetAllAsync())
                .ReturnsAsync(UserTestHelper.GetUserReadDtos());

        IActionResult result = await _controller.GetAll();

        var okObjectResult = result as OkObjectResult;

        var userReadDtos = okObjectResult?.Value as IEnumerable<UserReadDto>;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(userReadDtos?.Count(), Is.EqualTo(UserTestHelper.GetUserReadDtos().Count()));
        });
    }

    [Test]
    public async Task GetById_Success_ReturnOkObjectResult()
    {
        _userSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(UserTestHelper.GetUserReadDto());

        IActionResult result = await _controller.GetById(It.IsAny<Guid>());

        var okObjectResult = result as OkObjectResult;

        var userReadDto = okObjectResult?.Value as UserReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(userReadDto?.Id, Is.EqualTo(UserTestHelper.GetUserReadDto().Id));
        });
    }

    [Test]
    public async Task GetById_NotFoundException_ReturnNotFoundObjectResult()
    {
        _userSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("ex message"));

        IActionResult result = await _controller.GetById(It.IsAny<Guid>());

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

    [Test]
    public async Task GetMe_Success_ReturnOkObjectResult()
    {
        _userSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(UserTestHelper.GetUserReadDto());
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
           .WithDefaultIdentityClaims()
           .Build();

        IActionResult result = await _controller.GetMe();

        var okObjectResult = result as OkObjectResult;

        var userReadDto = okObjectResult?.Value as UserReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(userReadDto?.Id, Is.EqualTo(UserTestHelper.GetUserReadDto().Id));
        });
    }

    [Test]
    public async Task GetMe_NotFoundException_ReturnNotFoundObjectResult()
    {
        _userSrv
            .Setup(t => t.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("ex message"));
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.GetMe();

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

    [Test]
    public async Task GetUserCardBalance_Success_ReturnOkObjectResult()
    {
        _userSrv
            .Setup(t => t.GetCardBalanceReadDtoAsync(It.IsAny<Guid>()))
            .ReturnsAsync(CardBalanceTestHelper.GetCardBalanceReadDto());

        IActionResult result = await _controller.GetUserCardBalance(It.IsAny<Guid>());

        var okObjectResult = result as OkObjectResult;

        var cardBalanceReadDto = okObjectResult?.Value as CardBalanceReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(cardBalanceReadDto?.Id, Is.EqualTo(CardBalanceTestHelper.GetCardBalanceReadDto().Id));
        });
    }

    [Test]
    public async Task GetUserCardBalance_NotFoundException_ReturnNotFoundObjectResult()
    {
        _userSrv
            .Setup(t => t.GetCardBalanceReadDtoAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("ex message"));

        IActionResult result = await _controller.GetUserCardBalance(It.IsAny<Guid>());

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

    [Test]
    public async Task GetMeCardBalance_Success_ReturnOkObjectResult()
    {
        _userSrv
            .Setup(t => t.GetCardBalanceReadDtoAsync(It.IsAny<Guid>()))
            .ReturnsAsync(CardBalanceTestHelper.GetCardBalanceReadDto());
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.GetMeCardBalance();

        var okObjectResult = result as OkObjectResult;

        var cardBalanceReadDto = okObjectResult?.Value as CardBalanceReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(cardBalanceReadDto?.Id, Is.EqualTo(CardBalanceTestHelper.GetCardBalanceReadDto().Id));
        });
    }

    [Test]
    public async Task GetMeCardBalance_NotFoundException_ReturnNotFoundObjectResult()
    {
        _userSrv
            .Setup(t => t.GetCardBalanceReadDtoAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("ex message"));
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.GetMeCardBalance();

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

    [Test]
    public async Task GetUserPaymentDue_Success_ReturnOkObjectResult()
    {
        _userSrv
            .Setup(t => t.GetPaymentDueReadDtoAsync(It.IsAny<Guid>()))
            .ReturnsAsync(PaymentDueTestHelper.GetPaymentDueReadDto());

        IActionResult result = await _controller.GetUserPaymentDue(It.IsAny<Guid>());

        var okObjectResult = result as OkObjectResult;

        var paymentDueReadDto = okObjectResult?.Value as PaymentDueReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(paymentDueReadDto?.Id, Is.EqualTo(PaymentDueTestHelper.GetPaymentDueReadDto().Id));
        });
    }

    [Test]
    public async Task GetUserPaymentDue_NotFoundException_ReturnNotFoundObjectResult()
    {
        _userSrv
            .Setup(t => t.GetPaymentDueReadDtoAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("ex message"));

        IActionResult result = await _controller.GetUserPaymentDue(It.IsAny<Guid>());

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

    [Test]
    public async Task GetMePaymentDue_Success_ReturnOkObjectResult()
    {
        _userSrv
            .Setup(t => t.GetPaymentDueReadDtoAsync(It.IsAny<Guid>()))
            .ReturnsAsync(PaymentDueTestHelper.GetPaymentDueReadDto());
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.GetMePaymentDue();

        var okObjectResult = result as OkObjectResult;

        var paymentDueReadDto = okObjectResult?.Value as PaymentDueReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(paymentDueReadDto?.Id, Is.EqualTo(PaymentDueTestHelper.GetPaymentDueReadDto().Id));
        });
    }

    [Test]
    public async Task GetMePaymentDue_NotFoundException_ReturnNotFoundObjectResult()
    {
        _userSrv
            .Setup(t => t.GetPaymentDueReadDtoAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("ex message"));
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.GetMePaymentDue();

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

    [Test]
    public async Task GetUserDailyPoint_Success_ReturnOkObjectResult()
    {
        _userSrv
            .Setup(t => t.GetDailyPointReadDtoAsync(It.IsAny<Guid>()))
            .ReturnsAsync(DailyPointTestHelper.GetDailyPointReadDto());

        IActionResult result = await _controller.GetUserDailyPoint(It.IsAny<Guid>());

        var okObjectResult = result as OkObjectResult;

        var dailyPointReadDto = okObjectResult?.Value as DailyPointReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(dailyPointReadDto?.Id, Is.EqualTo(DailyPointTestHelper.GetDailyPointReadDto().Id));
        });
    }

    [Test]
    public async Task GetUserDailyPoint_NotFoundException_ReturnNotFoundObjectResult()
    {
        _userSrv
            .Setup(t => t.GetDailyPointReadDtoAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("ex message"));

        IActionResult result = await _controller.GetUserDailyPoint(It.IsAny<Guid>());

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

    [Test]
    public async Task GetMeDailyPoint_Success_ReturnOkObjectResult()
    {
        _userSrv
            .Setup(t => t.GetDailyPointReadDtoAsync(It.IsAny<Guid>()))
            .ReturnsAsync(DailyPointTestHelper.GetDailyPointReadDto());
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.GetMeDailyPoint();

        var okObjectResult = result as OkObjectResult;

        var dailyPointReadDto = okObjectResult?.Value as DailyPointReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(dailyPointReadDto?.Id, Is.EqualTo(DailyPointTestHelper.GetDailyPointReadDto().Id));
        });
    }

    [Test]
    public async Task GetMeDailyPoint_NotFoundException_ReturnNotFoundObjectResult()
    {
        _userSrv
            .Setup(t => t.GetDailyPointReadDtoAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("ex message"));
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.GetMeDailyPoint();

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

    [Test]
    public async Task GetUserTransactions_Success_ReturnOkObjectResult()
    {
        _userSrv
            .Setup(t => t.GetTransactionReadDtosPageAsync(It.IsAny<Guid>(), It.IsAny<PageParameters>()))
            .ReturnsAsync(TransactionTestHelper.GetTransactionReadDtoPage());

        IActionResult result = await _controller.GetUserTransactions(It.IsAny<Guid>(), new());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDtosPage = okObjectResult?.Value as PagedList<TransactionReadDto>;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(transactionReadDtosPage?.Count, Is.EqualTo(TransactionTestHelper.GetTransactionReadDtoPage().Count));
        });
    }

    [Test]
    public async Task GetUserTransactions_NotFoundException_ReturnNotFoundObjectResult()
    {
        _userSrv
            .Setup(t => t.GetTransactionReadDtosPageAsync(It.IsAny<Guid>(), It.IsAny<PageParameters>()))
            .ThrowsAsync(new NotFoundException("ex message"));

        IActionResult result = await _controller.GetUserTransactions(It.IsAny<Guid>(), new());

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }


    [Test]
    public async Task GetMeUserTransactions_Success_ReturnOkObjectResult()
    {
        _userSrv
            .Setup(t => t.GetTransactionReadDtosPageAsync(It.IsAny<Guid>(), It.IsAny<PageParameters>()))
            .ReturnsAsync(TransactionTestHelper.GetTransactionReadDtoPage());
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.GetMeTransactions(new());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDtosPage = okObjectResult?.Value as PagedList<TransactionReadDto>;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(transactionReadDtosPage?.Count, Is.EqualTo(TransactionTestHelper.GetTransactionReadDtoPage().Count));
        });
    }

    [Test]
    public async Task GetMeUserTransactions_NotFoundException_ReturnNotFoundObjectResult()
    {
        _userSrv
            .Setup(t => t.GetTransactionReadDtosPageAsync(It.IsAny<Guid>(), It.IsAny<PageParameters>()))
            .ThrowsAsync(new NotFoundException("ex message"));
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.GetMeTransactions(new());

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

    [Test]
    public async Task AddTransaction_Success_ReturnOkObjectResult()
    {
        _mapper
            .Setup(t => t.Map<TransactionAddDto>(It.IsAny<TransactionAddRequest>()))
            .Returns(TransactionTestHelper.GetTransactionAddDto());
        _transactionSrv
            .Setup(t => t.AddAsync(It.IsAny<TransactionAddDto>()))
            .ReturnsAsync(TransactionTestHelper.GetTransactionReadDto());
        _controller.ControllerContext = new ControllerContextBuilder().Build();

        IActionResult result = await _controller.AddTransaction(It.IsAny<Guid>(), It.IsAny<TransactionAddRequest>());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDto = okObjectResult?.Value as TransactionReadDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(transactionReadDto?.Id, Is.EqualTo(TransactionTestHelper.GetTransactionReadDto().Id));
        });
    }

    [Test]
    public async Task AddTransaction_SuccessIsAuthenticated_ReturnUserNameIsNotNull()
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

        IActionResult result = await _controller.AddTransaction(It.IsAny<Guid>(), It.IsAny<TransactionAddRequest>());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDto = okObjectResult?.Value as TransactionReadDto;

        var username = transactionReadDto?.SenderName;

        Assert.That(username, Is.Not.Null);
    }

    [Test]
    public async Task AddTransaction_SuccessIsNotAuthenticated_ReturnUserNameIsNull()
    {
        TransactionAddDto transactionAddDto = new();
        _mapper
            .Setup(t => t.Map<TransactionAddDto>(It.IsAny<TransactionAddRequest>()))
            .Returns(transactionAddDto);

        _transactionSrv
            .Setup(t => t.AddAsync(It.IsAny<TransactionAddDto>()))
            .ReturnsAsync(() => TransactionTestHelper.GetTransactionReadDtoFromAdd(transactionAddDto));

        _controller.ControllerContext = new ControllerContextBuilder().Build();

        IActionResult result = await _controller.AddTransaction(It.IsAny<Guid>(), It.IsAny<TransactionAddRequest>());

        var okObjectResult = result as OkObjectResult;

        var transactionReadDto = okObjectResult?.Value as TransactionReadDto;

        var username = transactionReadDto?.SenderName;

        Assert.That(username, Is.Null);
    }

    [Test]
    public async Task AddTransaction_NotFoundException_ReturnNotFoundObjectResult()
    {
        TransactionAddDto transactionAddDto = new();

        _mapper
            .Setup(t => t.Map<TransactionAddDto>(It.IsAny<TransactionAddRequest>()))
            .Returns(transactionAddDto);
        _transactionSrv
            .Setup(t => t.AddAsync(It.IsAny<TransactionAddDto>()))
            .ThrowsAsync(new NotFoundException("ex message"));
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.AddTransaction(It.IsAny<Guid>(), It.IsAny<TransactionAddRequest>());

        var notFoundObjectResult = result as NotFoundObjectResult;

        var errorResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }

    [Test]
    public async Task AddTransaction_TransactionException_ReturnBadRequestObjectResult()
    {
        TransactionAddDto transactionAddDto = new();

        _mapper
            .Setup(t => t.Map<TransactionAddDto>(It.IsAny<TransactionAddRequest>()))
            .Returns(transactionAddDto);
        _transactionSrv
            .Setup(t => t.AddAsync(It.IsAny<TransactionAddDto>()))
            .ThrowsAsync(new TransactionException("ex message"));
        _controller.ControllerContext = new ControllerContextBuilder("TestAuthenticationType")
            .WithDefaultIdentityClaims()
            .Build();

        IActionResult result = await _controller.AddTransaction(It.IsAny<Guid>(), It.IsAny<TransactionAddRequest>());

        var badRequestObjectResult = result as BadRequestObjectResult;

        var errorResponse = badRequestObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(errorResponse?.Error, Is.EqualTo("ex message"));
        });
    }
}


