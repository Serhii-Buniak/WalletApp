using Microsoft.AspNetCore.Http;
using WalletApp.Common.Enums;
using WalletApp.Common.Mapping;
using WalletApp.DAL.Entities;

namespace WalletApp.BLL.Dtos.TransactionDtos;

public class TransactionAddDto : IMapTo<Transaction>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Sum { get; set; }
    public string Description { get; set; } = null!;
    public bool IsPending { get; set; }
    public TransactionType Type { get; set; }
    public Guid? SenderId { get; set; }
}
