using WalletApp.DAL.Entities.Common;

namespace WalletApp.DAL.Entities;

public class Image : BaseEntity
{
    public override long Id { get; set; }
    public string Name { get; set; } = null!;
}
