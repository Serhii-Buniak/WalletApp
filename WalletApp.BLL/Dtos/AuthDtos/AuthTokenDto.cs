namespace WalletApp.BLL.Dtos.AuthDtos;

public class AuthTokenDto : IEquatable<AuthTokenDto>
{
    public string Access { get; set; } = null!;

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is AuthTokenDto authTokenDto)
        {
            return Equals(authTokenDto);
        }

        return false;
    }

    public bool Equals(AuthTokenDto? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other == null)
        {
            return false;
        }

        return other.Access == Access;
    }
}
