using AutoMapper;
using Moq;
using System.Text;
using WalletApp.Common.Mapping.ValueConverters;

namespace WalletApp.Common.Tests.ValueConverters;

internal class DailyPointIntToStringValueConverterTests
{
    private DailyPointIntToStringValueConverter _converter;

    [SetUp]
    public void Setup()
    {
        _converter = new DailyPointIntToStringValueConverter();
    }

    [TestCase(0)]
    [TestCase(500)]
    [TestCase(999)]
    public void Convert_Less1000_ReturnSimpleNumber(int number)
    {
        var result = _converter.Convert(number, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo(number.ToString()));
    }

    [Test]
    public void Convert_1000_ReturnKNumber()
    {
        var result = _converter.Convert(1000, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo("1K"));
    }

    [Test]
    public void Convert_1499_Return1KNumber()
    {
        var result = _converter.Convert(1499, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo("1K"));
    }    
    
    [Test]
    public void Convert_1500_Return2KNumber()
    {
        var result = _converter.Convert(1500, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo("2K"));
    }    
    
    [Test]
    public void Convert_1501_Return2KNumber()
    {
        var result = _converter.Convert(1501, It.IsAny<ResolutionContext>());

        Assert.That(result, Is.EqualTo("2K"));
    }
}
