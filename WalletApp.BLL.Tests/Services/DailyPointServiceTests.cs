using Moq;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.BLL.Services.Realizations;

namespace WalletApp.BLL.Tests.Services;

internal class DailyPointServiceTests
{
    private const double dayPoint5 = 6.968;
    private const double dayPoint32 = 20558.901182599322;

    private Mock<IDateTimeService> _dateTimeSrv;
    private DailyPointService _dailyPointSrv;


    [SetUp]
    public void Setup()
    {
        _dateTimeSrv = new Mock<IDateTimeService>();

        _dailyPointSrv = new DailyPointService(_dateTimeSrv.Object);
    }

    [Test]
    public void Get_Spring_GetDaylyPoints()
    {
        _dateTimeSrv
            .Setup(p => p.Now)
            .Returns(new DateTime(2000, 3, 5));

        var dailyPoint = _dailyPointSrv.Get();

        Assert.That(dailyPoint.Count, Is.EqualTo(dayPoint5));
    }

    [Test]
    public void Get_Summer_GetDaylyPoints()
    {
        _dateTimeSrv
            .Setup(p => p.Now)
            .Returns(new DateTime(2000, 6, 5));

        var dailyPoint = _dailyPointSrv.Get();

        Assert.That(dailyPoint.Count, Is.EqualTo(dayPoint5));
    }   
    
    [Test]
    public void Get_Autumn_GetDaylyPoints()
    {
        _dateTimeSrv
            .Setup(p => p.Now)
            .Returns(new DateTime(2000, 9, 5));

        var dailyPoint = _dailyPointSrv.Get();

        Assert.That(dailyPoint.Count, Is.EqualTo(dayPoint5));
    }    
    
    [Test]
    public void Get_Winter_GetDaylyPoints()
    {
        _dateTimeSrv
            .Setup(p => p.Now)
            .Returns(new DateTime(2000, 12, 5));

        var dailyPoint = _dailyPointSrv.Get();

        Assert.That(dailyPoint.Count, Is.EqualTo(dayPoint5));
    }
    
    [Test]
    public void Get_WinterNextYear_GetDaylyPoints()
    {
        _dateTimeSrv
            .Setup(p => p.Now)
            .Returns(new DateTime(2000, 1, 1));

        var dailyPoint = _dailyPointSrv.Get();

        Assert.That(dailyPoint.Count, Is.EqualTo(dayPoint32));
    }    
    
    [Test]
    public void Get_OneDay_GetDaylyPoints()
    {
        _dateTimeSrv
            .Setup(p => p.Now)
            .Returns(new DateTime(2000, 3, 1));

        var dailyPoint = _dailyPointSrv.Get();

        Assert.That(dailyPoint.Count, Is.EqualTo(2));
    }    

    [Test]
    public void Get_TwoDay_GetDaylyPoints()
    {
        _dateTimeSrv
            .Setup(p => p.Now)
            .Returns(new DateTime(2000, 3, 2));

        var dailyPoint = _dailyPointSrv.Get();

        Assert.That(dailyPoint.Count, Is.EqualTo(3));
    }
}
