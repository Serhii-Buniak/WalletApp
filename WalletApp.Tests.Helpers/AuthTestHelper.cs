using WalletApp.BLL.Dtos.AuthDtos;

namespace WalletApp.Tests.TestHelpers;

public static class AuthTestHelper
{
    public static LogInDto GetLogInDto()
    {
        return new LogInDto
        {
            UserName = UserTestHelper.GetUser().UserName!,
            Password = "password"
        };
    }   
    
    public static RegisterDto GetRegisterDto()
    {
        return new RegisterDto
        {
            UserName = "username",
            Password = "password"
        };
    }

    public static AuthTokenDto GetAuthTokenDto()
    {
        return new AuthTokenDto
        {
            Access = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI2ZjdlNGFkMC0yODQ4LTQxN2MtOTIyMC1lNzExZGVjNjkwODYiLCJzdWIiOiJzdHJpbmciLCJqdGkiOiJhN2NkOWNlNC0xZDQ0LTQyNWEtYjc1ZC1kNzZhZWViZjE0ZGEiLCJleHAiOjE2OTEwMDAzNjMsImlzcyI6IlNlY3VyZUFwaSIsImF1ZCI6IlNlY3VyZUFwaVVzZXIifQ.1Wm62ErFCFkutYxDV1ccAVPFjGyxJdrtEY3b9gGwA48",
        };
    }
}
