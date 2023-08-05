using AutoMapper;
using WalletApp.BLL.Services.Interfaces;

namespace WalletApp.BLL.ValueConverters;

public sealed class DailyPointIntToStringValueConverter : IValueConverter<double, string>
{
    public DailyPointIntToStringValueConverter()
    {

    }

    public string Convert(double sourceMember, ResolutionContext context)
    {
        return Convert(sourceMember);
    }   
    
    public string Convert(double sourceMember)
    {
        sourceMember = Math.Round(sourceMember);

        if (1000 > sourceMember)
        {
            return sourceMember.ToString();
        }

        double thousandCount = Math.Round(sourceMember / 1000);

        return thousandCount + "K";
    }
}