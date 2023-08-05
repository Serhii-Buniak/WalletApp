using AutoMapper;
using Moq;
using WalletApp.BLL.ValueConverters;

namespace WalletApp.BLL.Tests.ValueConverters;

internal class DateTimeToStringMonthIValueConverterTests
{
    private DateTimeToStringMonthIValueConverter _converter;

    [SetUp]
    public void Setup()
    {
        _converter = new DateTimeToStringMonthIValueConverter();
    }

    [Test]
    public void Convert_Success_ReturnStringMonth()
    {
        var result = _converter.Convert(new DateTime(2000, 1, 1), It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo("January"));
    }
}
