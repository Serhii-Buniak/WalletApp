using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Dtos.AuthDtos;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Tests.TestHelpers;
using WalletApp.WebApi.Controllers;
using WalletApp.WebApi.Responses;

namespace WalletApp.WebApi.Tests.Controllers;

internal class AuthControllerTests
{
    private Mock<IAuthService> _authSrv;
    private AuthController _controller;

    [SetUp]
    public void Setup()
    {
        _authSrv = new Mock<IAuthService>();

        _controller = new AuthController(_authSrv.Object);
    }

    [Test]
    public async Task Register_Success_ReturnOkResult()
    {
        _authSrv
            .Setup(t => t.RegisterAsync(It.IsAny<RegisterDto>()));

        IActionResult result = await _controller.Register(It.IsAny<RegisterDto>());

        Assert.That(result, Is.InstanceOf<OkResult>());
    }   
    
    [Test]
    public async Task Register_ValidationModelException_ReturnBadRequestObjectResult()
    {
        List<IdentityError> identityErrors = new() 
        {
            new (){ Code = "Code", Description = "Description"}
        };

        _authSrv
            .Setup(t => t.RegisterAsync(It.IsAny<RegisterDto>()))
            .ThrowsAsync(new ValidationModelException(identityErrors));

        IActionResult result = await _controller.Register(It.IsAny<RegisterDto>());

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }    
    
    [Test]
    public async Task Register_ValidationModelException_ReturnExMessage()
    {
        List<IdentityError> identityErrors = new() 
        {
            new (){ Code = "Code", Description = "Description"}
        };

        _authSrv
            .Setup(t => t.RegisterAsync(It.IsAny<RegisterDto>()))
            .ThrowsAsync(new ValidationModelException(identityErrors));

        IActionResult result = await _controller.Register(It.IsAny<RegisterDto>());

        var badRequestObjectResult = result as BadRequestObjectResult;

        var badRequestObjectResponse = badRequestObjectResult?.Value as IDictionary<string, string[]>;


        Assert.That(badRequestObjectResponse?.Count, Is.EqualTo(identityErrors.Count));
    }
    
    [Test]
    public async Task LogIn_Success_ReturnOkObjectResult()
    {
        _authSrv
            .Setup(t => t.LogInAsync(It.IsAny<LogInDto>()))
            .ReturnsAsync(AuthTestHelper.GetAuthTokenDto());

        IActionResult result = await _controller.LogIn(It.IsAny<LogInDto>());

        var okObjectResult = result as OkObjectResult;

        var authTokenDto = okObjectResult?.Value as AuthTokenDto;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(authTokenDto, Is.EqualTo(AuthTestHelper.GetAuthTokenDto()));
        });
    }    
    
    [Test]
    public async Task LogIn_NotFoundException_ReturnNotFoundObjectResult()
    {
        _authSrv
            .Setup(t => t.LogInAsync(It.IsAny<LogInDto>()))
            .ThrowsAsync(new NotFoundException("ex message"));

        IActionResult result = await _controller.LogIn(It.IsAny<LogInDto>());

        var notFoundObjectResult = result as NotFoundObjectResult;

        var notFoundResponse = notFoundObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            Assert.That(notFoundResponse?.Error, Is.EqualTo("ex message"));
        });
    }  
    
    [Test]
    public async Task LogIn_ValidationModelException_ReturnBadRequestObjectResult()
    {
        _authSrv
            .Setup(t => t.LogInAsync(It.IsAny<LogInDto>()))
            .ThrowsAsync(new ValidationModelException());

        IActionResult result = await _controller.LogIn(It.IsAny<LogInDto>());

        var badRequestObjectResult = result as BadRequestObjectResult;

        var badRequestResponse = badRequestObjectResult?.Value as ErrorResponse;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(badRequestResponse?.Error, Is.EqualTo("Password uncorrect"));
        });
    }

}