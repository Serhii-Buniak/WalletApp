using System.Security.Cryptography;

namespace WalletApp.DAL.Entities.Common;

public interface IEntity<TId> where TId : notnull
{
    public abstract TId Id { get; set; }
    public DateTime CreatedAt { get; set; }
}

public interface IEntity : IEntity<long>
{
}

