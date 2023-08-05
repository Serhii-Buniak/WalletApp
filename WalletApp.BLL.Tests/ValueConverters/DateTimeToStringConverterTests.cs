using AutoMapper;
using Moq;
using System.Globalization;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.BLL.ValueConverters;

namespace WalletApp.BLL.Tests.ValueConverters;

internal class DateTimeToStringConverterTests
{
    private readonly DateTime fakeNow = new(2000, 1, 1);
    private DateTimeToStringConverter _converter;

    [SetUp]
    public void Setup()
    {
        _converter = new DateTimeToStringConverter();
    }

    [Test]
    public void Convert_MinuteAgo_ReturnNow()
    {
        var now = fakeNow;

        DateTime minuteAgo = now - TimeSpan.FromMinutes(1) + TimeSpan.FromMilliseconds(1);

        var result = _converter.Convert(minuteAgo, now);

        Assert.That(result, Is.EqualTo("now"));
    }

    [Test]
    public void Convert_HourAgo_ReturnMinute()
    {
        var now = fakeNow;

        DateTime hourAgo = now - TimeSpan.FromHours(1) + TimeSpan.FromMilliseconds(1);

        var result = _converter.Convert(hourAgo, now);

        Assert.That(result, Is.EqualTo("59m"));
    }

    [Test]
    public void Convert_DayAgo_ReturnHours()
    {
        var now = fakeNow;

        DateTime dayAgo = now - TimeSpan.FromDays(1) + TimeSpan.FromMilliseconds(1);

        var result = _converter.Convert(dayAgo, now);

        Assert.That(result, Is.EqualTo("23h"));
    }

    [Test]
    public void Convert_TwoDayAgo_ReturnYesterday()
    {
        var now = fakeNow;

        DateTime twoDayAgo = now - TimeSpan.FromDays(2) + TimeSpan.FromMilliseconds(1);

        var result = _converter.Convert(twoDayAgo, now);

        Assert.That(result, Is.EqualTo("Yesterday"));
    }

    [Test]
    public void Convert_WeekAgo_ReturnDay()
    {
        var now = fakeNow;

        DateTime weekAgo = now - TimeSpan.FromDays(7) + TimeSpan.FromMilliseconds(1);

        var result = _converter.Convert(weekAgo, now);

        Assert.That(result, Is.EqualTo(weekAgo.ToString("dddd", CultureInfo.GetCultureInfo("en"))));
    }

    [Test]
    public void Convert_MoreThanWeekAgo_ReturnDateFormat()
    {
        var now = fakeNow;

        DateTime moreThanWeekAgo = now - TimeSpan.FromDays(7);

        var result = _converter.Convert(moreThanWeekAgo, now);

        Assert.That(result, Is.EqualTo(moreThanWeekAgo.ToString("d", CultureInfo.GetCultureInfo("en"))));
    }
}
