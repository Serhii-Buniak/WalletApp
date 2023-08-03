using WalletApp.DAL.Entities.Common;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.DAL.Entities;

public class DailyPoint : BaseEntity
{
    public override long Id { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public int Count { get; set; }
}