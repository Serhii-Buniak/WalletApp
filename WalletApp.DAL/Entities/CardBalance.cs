using WalletApp.DAL.Entities.Common;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.DAL.Entities;

public class CardBalance : BaseEntity
{
    public const decimal Max = 1500;
      
    public override long Id { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public decimal Sum { get; set; }
    public decimal Available => Max - Sum;
}
