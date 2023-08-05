using AutoMapper;
using WalletApp.Common.Enums;
using WalletApp.Common.Mapping;
using WalletApp.DAL.Entities;

namespace WalletApp.BLL.Dtos.TransactionDtos;

public class TransactionReadDto : IMapFrom<Transaction>
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Sum { get; set; }
    public string Description { get; set; } = null!;
    public bool IsPending { get; set; }
    public TransactionType Type { get; set; }
    public string WasCreated { get; set; } = null!;
    public string? SenderName { get; set; }

    void IMapFrom<Transaction>.Mapping(Profile profile)
    {
        profile
            .CreateMap<Transaction, TransactionReadDto>()
            .AfterMap((src, dest) =>
            {
                dest.SenderName = src.Sender?.UserName;
            });
    }
}

