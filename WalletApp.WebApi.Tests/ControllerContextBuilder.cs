using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WalletApp.Tests.TestHelpers;

namespace WalletApp.WebApi.Tests;

public class ControllerContextBuilder
{
    private readonly ClaimsIdentity _identity;
    private readonly ClaimsPrincipal _user;

    public ControllerContextBuilder()
    {
        _identity = new ClaimsIdentity();
        _user = new ClaimsPrincipal(_identity);
    }    
    
    public ControllerContextBuilder(string authenticationType)
    {
        _identity = new ClaimsIdentity(authenticationType);
        _user = new ClaimsPrincipal(_identity);
    }

    public ControllerContextBuilder WithClaims(IDictionary<string, string> claims)
    {
        _identity.AddClaims(claims.Select(c => new Claim(c.Key, c.Value)));
        return this;
    }

    public ControllerContextBuilder WithIdentity(string userId, string userName)
    {
        _identity.AddClaims(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName)
        });
        return this;
    }


    public ControllerContextBuilder WithDefaultIdentityClaims()
    {
        _identity.AddClaims(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, UserTestHelper.GetUserReadDto().Id.ToString()),
            new Claim(ClaimTypes.Name,  UserTestHelper.GetUserReadDto().UserName)
        });

        return this;
    }

    public ControllerContext Build()
    {
        return new ControllerContext { HttpContext = new DefaultHttpContext { User = _user } }; ;
    }
}