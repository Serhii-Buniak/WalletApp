using WalletApp.Common.Exceptions;

namespace WalletApp.WebApi.Responses;

public class ErrorResponse
{
    public ErrorResponse(string error)
    {
        Error = error;
    }

    public string Error { get; set; }

    public static ErrorResponse Create(string error)
    {
        return new ErrorResponse(error);
    }    
    
    public static ErrorResponse Create(NotFoundException exception)
    {
        return new ErrorResponse(exception.Message);
    }
}
