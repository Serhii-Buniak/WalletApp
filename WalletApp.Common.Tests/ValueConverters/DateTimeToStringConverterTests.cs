using AutoMapper;
using Moq;
using System.Globalization;
using WalletApp.Common.Mapping.ValueConverters;

namespace WalletApp.Common.Tests.ValueConverters;

internal class DateTimeToStringConverterTests
{
    private DateTimeToStringConverter _converter;

    [SetUp]
    public void Setup()
    {
        _converter = new DateTimeToStringConverter();
    }

    [Test]
    public void Convert_MinuteAgo_ReturnNow()
    {
        var now = DateTime.UtcNow;

        DateTime minuteAgo = now - TimeSpan.FromMinutes(0.99);

        var result = _converter.Convert(minuteAgo, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo("now"));
    }

    [Test]
    public void Convert_HourAgo_ReturnMinute()
    {
        var now = DateTime.UtcNow;

        DateTime hourAgo = now - TimeSpan.FromHours(0.99);

        var result = _converter.Convert(hourAgo, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo("59m"));
    }

    [Test]
    public void Convert_DayAgo_ReturnHours()
    {
        var now = DateTime.UtcNow;

        DateTime dayAgo = now - TimeSpan.FromDays(0.99);

        var result = _converter.Convert(dayAgo, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo("23h"));
    }

    [Test]
    public void Convert_TwoDayAgo_ReturnYesterday()
    {
        var now = DateTime.UtcNow;

        DateTime twoDayAgo = now - TimeSpan.FromDays(1.99);

        var result = _converter.Convert(twoDayAgo, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo("Yesterday"));
    }

    [Test]
    public void Convert_WeekAgo_ReturnDay()
    {
        var now = DateTime.UtcNow;

        DateTime weekAgo = now - TimeSpan.FromDays(6.99);

        var result = _converter.Convert(weekAgo, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo(weekAgo.ToString("dddd", CultureInfo.GetCultureInfo("en"))));
    }   
    
    [Test]
    public void Convert_MoreThanWeekAgo_ReturnDateFormat()
    {
        var now = DateTime.UtcNow;

        DateTime moreThanWeekAgo = now - TimeSpan.FromDays(7.99);

        var result = _converter.Convert(moreThanWeekAgo, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo(moreThanWeekAgo.ToString("d", CultureInfo.GetCultureInfo("en"))));
    }
}
