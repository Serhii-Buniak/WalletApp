using Microsoft.AspNetCore.Identity;

namespace WalletApp.Common.Exceptions;

public class InvalidClaimsPrincipal : Exception
{
    public InvalidClaimsPrincipal()
    {
    }

    public InvalidClaimsPrincipal(string? message) : base(message)
    {
    }

    public InvalidClaimsPrincipal(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
public class ValidationModelException : Exception
{
    public ValidationModelException()
    : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationModelException(IEnumerable<IdentityError> errors)
        : this()
    {
        Errors = errors
            .GroupBy(e => e.Code, e => e.Description)
            .ToDictionary(errorGroup => errorGroup.Key, errorGroup => errorGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
