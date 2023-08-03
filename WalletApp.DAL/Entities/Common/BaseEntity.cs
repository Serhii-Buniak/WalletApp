namespace WalletApp.DAL.Entities.Common;

public abstract class BaseEntity<TId> : IEntity<TId> where TId : notnull
{
    public static DateTime CreatedAtDefaultImplentation => DateTime.UtcNow;

    public abstract TId Id { get; set; }
    public DateTime CreatedAt { get; set; } = CreatedAtDefaultImplentation;
}

public abstract class BaseEntity : BaseEntity<long>
{

}