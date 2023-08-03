using AutoMapper;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.Common.Mapping;
using WalletApp.Common.Mapping.ValueConverters;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Common;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.BLL.Dtos.DailyPointDtos;

public class DailyPointReadDto : IMapFrom<DailyPoint>
{
    public long Id { get; set; }

    public int Count { get; set; }
    public string DisplayCount { get; set; } = null!;

    void IMapFrom<DailyPoint>.Mapping(Profile profile)
    {
        profile
            .CreateMap<DailyPoint, DailyPointReadDto>()
            .ForMember(
                p => p.DisplayCount,
                s => s.ConvertUsing(new DailyPointIntToStringValueConverter(), src => src.Count)
            );
    }
}
