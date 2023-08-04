using Microsoft.AspNetCore.Identity;
using WalletApp.DAL.Entities.Common;

namespace WalletApp.DAL.Entities.Identity;

public class AppUser : IdentityUser<Guid>, IEntity<Guid>
{
    public ICollection<Transaction> Transactions = new List<Transaction>();
    public CardBalance CardBalance { get; set; } = new();
    public PaymentDue PaymentDue { get; set; } = new();
    public DailyPoint DailyPoint { get; set; } = new();
    public DateTime CreatedAt { get; set; } = BaseEntity.CreatedAtDefaultImplentation;
}
