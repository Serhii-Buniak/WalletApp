using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WalletApp.BLL.Dtos.AuthDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.BLL.Settings;
using WalletApp.Common.Exceptions;
using WalletApp.DAL;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.BLL.Services.Realizations;

public class AuthService : IAuthService
{
    private readonly IUserAppManager _userManager;
    private readonly JwtSettings _jwt;
    private readonly IMapper _mapper;

    public AuthService(IUserAppManager userManager, IOptions<JwtSettings> jwt, IMapper mapper)
    {
        _userManager = userManager;
        _jwt = jwt.Value;
        _mapper = mapper;
    }

    public async Task<AuthTokenDto> LogInAsync(LogInDto logInDto)
    {
        AuthTokenDto token = new();

        AppUser? user = await _userManager.FindByNameAsync(logInDto.UserName);
        if (user == null)
        {
            throw new NotFoundException(nameof(AppUser), logInDto.UserName);
        }

        bool canLogIn = await _userManager.CheckPasswordAsync(user, logInDto.Password);
        if (!canLogIn)
        {
            throw new AuthException("Uncorrect password");
        }

        JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
        token.Access = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }

    public async Task RegisterAsync(RegisterDto registerDto)
    {
        var user = _mapper.Map<AppUser>(registerDto);

        IdentityResult createUserResult = await _userManager.CreateAsync(user, registerDto.Password);
        if (!createUserResult.Succeeded)
        {
            throw new ValidationModelException(createUserResult.Errors);
        }
    }

    private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
    {
        IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
        IList<string> roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", roles[i]));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        }.Union(userClaims).Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken
        (
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials
        );

        return jwtSecurityToken;
    }
}