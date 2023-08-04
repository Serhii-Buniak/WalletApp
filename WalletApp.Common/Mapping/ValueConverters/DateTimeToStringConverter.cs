using AutoMapper;
using System.Globalization;

namespace WalletApp.Common.Mapping.ValueConverters;

public sealed class DateTimeToStringConverter : IValueConverter<DateTime, string>
{
    public string Convert(DateTime createdAt, ResolutionContext context)
    {
        DateTime now = DateTime.UtcNow;

        TimeSpan difference = now - createdAt;

        TimeSpan minuteTimeSpan = TimeSpan.FromMinutes(1); 
        if (difference < minuteTimeSpan)
        {
            return "now";
        }           
        
        TimeSpan hourTimeSpan = TimeSpan.FromHours(1); 
        if (minuteTimeSpan < difference && difference < hourTimeSpan)
        {
            return Math.Truncate(difference.TotalMinutes) + "m";
        }            
        
        TimeSpan dayTimeSpan = TimeSpan.FromDays(1); 
        if (hourTimeSpan < difference && difference < dayTimeSpan)
        {
            return Math.Truncate(difference.TotalHours) + "h";
        }    
                
        TimeSpan twoDaysTimeSpan = TimeSpan.FromDays(2); 
        if (dayTimeSpan < difference && difference < twoDaysTimeSpan)
        {
            return "Yesterday";
        }    
        
        TimeSpan weekTimeSpan = TimeSpan.FromDays(7); 
        if (twoDaysTimeSpan < difference && difference < weekTimeSpan)
        {
            return createdAt.ToString("dddd", CultureInfo.GetCultureInfo("en"));
        }

        return createdAt.ToString("d", CultureInfo.GetCultureInfo("en"));
    }
}
