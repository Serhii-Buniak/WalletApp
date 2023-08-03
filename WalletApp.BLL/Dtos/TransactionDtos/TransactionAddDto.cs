using Microsoft.AspNetCore.Http;
using WalletApp.Common.Enums;

namespace WalletApp.BLL.Dtos.TransactionDtos;

public class TransactionAddDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Sum { get; set; }
    public string Description { get; set; } = null!;
    public bool IsPending { get; set; }
    public TransactionType Type { get; set; }
    public IFormFile? Icon { get; set; } = null!;
    public Guid? SenderId { get; set; }
}
