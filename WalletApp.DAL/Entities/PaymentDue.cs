using WalletApp.DAL.Entities.Common;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.DAL.Entities;

public class PaymentDue : BaseEntity
{
    public override long Id { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

#pragma warning disable CA1822
    public DateTime PaidAt => DateTime.UtcNow;
#pragma warning restore CA1822 
}
