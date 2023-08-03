using AutoMapper;

namespace WalletApp.Common.Mapping.ValueConverters;

public sealed class DailyPointIntToStringValueConverter : IValueConverter<int, string>
{
    public string Convert(int sourceMember, ResolutionContext context)
    {
        if (1000 > sourceMember)
        {
            return sourceMember.ToString();
        }

        double thousandCount = Math.Round((double)sourceMember / 1000);

        return thousandCount + "K";
    }
}