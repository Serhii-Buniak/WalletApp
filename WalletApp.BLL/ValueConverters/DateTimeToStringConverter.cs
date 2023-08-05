using AutoMapper;
using System.Globalization;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.BLL.Services.Realizations;

namespace WalletApp.BLL.ValueConverters;

public sealed class DateTimeToStringConverter 
{
    public string Convert(DateTime from, DateTime to)
    {
        TimeSpan difference = to - from;

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
            return from.ToString("dddd", CultureInfo.GetCultureInfo("en"));
        }

        return from.ToString("d", CultureInfo.GetCultureInfo("en"));
    }
}
