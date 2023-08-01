using WalletApp.Common.Exceptions;

namespace WalletApp.WebApi.Responses;

public class NotFoundResponse
{
    public NotFoundResponse(string error)
    {
        Error = error;
    }

    public string Error { get; set; }

    public static NotFoundResponse Create(string error)
    {
        return new NotFoundResponse(error);
    }    
    
    public static NotFoundResponse Create(NotFoundException exception)
    {
        return new NotFoundResponse(exception.Message);
    }
}
