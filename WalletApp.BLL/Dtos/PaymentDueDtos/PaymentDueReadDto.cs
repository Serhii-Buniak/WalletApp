using AutoMapper;
using WalletApp.Common.Mapping;
using WalletApp.Common.Mapping.ValueConverters;
using WalletApp.DAL.Entities;

namespace WalletApp.BLL.Dtos.PaymentDueDtos;

public class PaymentDueReadDto : IMapFrom<PaymentDue>
{
    public long Id { get; set; }
    public string Month { get; set; } = null!;
    public string Message => $"You’ve paid your {Month} balance.";

    void IMapFrom<PaymentDue>.Mapping(Profile profile)
    {
        profile
            .CreateMap<PaymentDue, PaymentDueReadDto>()
            .ForMember(
                p => p.Month,
                s => s.ConvertUsing(new DateTimeToStringMonthIValueConverter(), src => src.PaidAt)
            );
    }
}
