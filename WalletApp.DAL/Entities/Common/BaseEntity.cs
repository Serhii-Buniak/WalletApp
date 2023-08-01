namespace WalletApp.DAL.Entities.Common;

public abstract class BaseEntity<TId>
{
    public abstract TId Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public abstract class BaseEntity : BaseEntity<long>
{

}

