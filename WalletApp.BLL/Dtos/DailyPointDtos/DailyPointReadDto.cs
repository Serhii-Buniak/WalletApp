using AutoMapper;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.Common.Mapping;
using WalletApp.Common.Mapping.ValueConverters;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Common;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.BLL.Dtos.DailyPointDtos;

public record class DailyPointReadDto 
{
    public double Count { get; set; }
    public string DisplayCount { get; set; } = null!;
}
