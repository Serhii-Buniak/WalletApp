﻿using AutoMapper;
using System.Globalization;

namespace WalletApp.Common.Mapping.ValueConverters;

public sealed class DateTimeToStringMonthIValueConverter : IValueConverter<DateTime, string>
{
    public string Convert(DateTime sourceMember, ResolutionContext context)
    {
        return sourceMember.ToString("MMMM", CultureInfo.GetCultureInfo("en"));
    }
}
