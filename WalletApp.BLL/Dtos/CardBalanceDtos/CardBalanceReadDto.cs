using WalletApp.Common.Mapping;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.BLL.Dtos.CardBalanceDtos;

public class CardBalanceReadDto : IMapFrom<CardBalance>
{
    public long Id { get; set; }

    public decimal Sum { get; set; }
    public decimal Available { get; set; }
}
