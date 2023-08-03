using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System.Security.Claims;
using WalletApp.BLL.Dtos.AuthDtos;
using WalletApp.BLL.Services.Realizations;
using WalletApp.BLL.Settings;
using WalletApp.Common.Exceptions;
using WalletApp.DAL.Entities.Identity;
using WalletApp.Tests.TestHelpers;

namespace WalletApp.BLL.Tests.Services;

internal class AuthServiceTests
{
    private Mock<IMapper> _mapper;
    private Mock<IOptions<JwtSettings>> _jwtOpt;
    private Mock<IUserAppManager> _userManager;

    private AuthService _authSrv;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mock<IMapper>();

        _jwtOpt = new Mock<IOptions<JwtSettings>>();
        _jwtOpt
            .Setup(j => j.Value)
            .Returns(new JwtSettings
            {
                Key = "11111111111111111111111111111",
                Issuer = "Issuer",
                Audience = "Audience",
                DurationInMinutes = 30
            });

        _userManager = new Mock<IUserAppManager>();
        _userManager
            .Setup(m => m.GetClaimsAsync(It.IsAny<AppUser>()))
            .ReturnsAsync(new List<Claim>());
        _userManager
            .Setup(m => m.GetRolesAsync(It.IsAny<AppUser>()))
            .ReturnsAsync(new List<string>());

        _authSrv = new AuthService(_userManager.Object, _jwtOpt.Object, _mapper.Object);
    }


    [Test]
    public async Task Register_Success_CreateAsyncWasCalled()
    {
        _mapper
            .Setup(m => m.Map<AppUser>(It.IsAny<RegisterDto>()));

        _userManager
            .Setup(m => m.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        await _authSrv.RegisterAsync(AuthTestHelper.GetRegisterDto());

        _userManager.Verify(m => m.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()));
    }

    [Test]
    public void Register_Failed_ThrowValidationModelException()
    {
        _mapper
            .Setup(m => m.Map<AppUser>(It.IsAny<RegisterDto>()));

        _userManager
            .Setup(m => m.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed());

        var registerAsync = async () => await _authSrv.RegisterAsync(AuthTestHelper.GetRegisterDto());

        Assert.ThrowsAsync<ValidationModelException>(async () =>
        {
            await registerAsync();
        });
    }

    [Test]
    public async Task LogIn_Success_RetutnAuthTokenDto()
    {
        _userManager
            .Setup(m => m.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(UserTestHelper.GetUser());
        _userManager
            .Setup(m => m.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        AuthTokenDto authTokenDto = await _authSrv.LogInAsync(AuthTestHelper.GetLogInDto());

        Assert.That(authTokenDto.Access, Is.Not.Empty);
    }

    [Test]
    public void LogIn_UserNotFound_ThrowAuthException()
    {
        _userManager
            .Setup(m => m.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(UserTestHelper.GetUser());

        _userManager
            .Setup(m => m.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(false);

        var logInAsync = async () => await _authSrv.LogInAsync(AuthTestHelper.GetLogInDto());

        Assert.ThrowsAsync<AuthException>(async () => await logInAsync());
    }
}
