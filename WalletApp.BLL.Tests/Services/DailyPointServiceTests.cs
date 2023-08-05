using AutoMapper;
using Moq;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Services.Realizations;
using WalletApp.Common.Exceptions;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories;
using WalletApp.Tests.Helpers;

namespace WalletApp.BLL.Tests.Services;

internal class DailyPointServiceTests
{
    private Mock<IMapper> _mapper;
    private Mock<IDataWrapper> _dataWrapper;

    private DailyPointService _dailyPointSrv;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mock<IMapper>();
        _dataWrapper = new Mock<IDataWrapper>();

        _dailyPointSrv = new DailyPointService(_dataWrapper.Object, _mapper.Object);
    }

    [Test]
    public async Task GetByIdAsync_Success_ReturnDailyPointReadDto()
    {
        _dataWrapper
            .Setup(d => d.DailyPoints.GetByIdOrDefaultAsync(It.IsAny<long>()))
            .ReturnsAsync(DailyPointTestHelper.GetDailyPoint());

        _mapper
            .Setup(m => m.Map<DailyPointReadDto>(It.IsAny<DailyPoint>()))
            .Returns(DailyPointTestHelper.GetDailyPointReadDto());

        DailyPointReadDto dailyPointReadDto = await _dailyPointSrv.GetByIdAsync(It.IsAny<long>());

        Assert.That(dailyPointReadDto.Id, Is.EqualTo(DailyPointTestHelper.GetDailyPoint().Id));
    }

    [Test]
    public void GetByIdAsync_NotFound_ThrowNotFoundException()
    {
        _dataWrapper
            .Setup(d => d.DailyPoints.GetByIdOrDefaultAsync(It.IsAny<long>()))
            .ThrowsAsync(new NotFoundException());

        _mapper
            .Setup(m => m.Map<DailyPointReadDto>(It.IsAny<DailyPoint>()))
            .Returns(DailyPointTestHelper.GetDailyPointReadDto());

        var getById = async () => await _dailyPointSrv.GetByIdAsync(It.IsAny<long>());

        Assert.ThrowsAsync<NotFoundException>(async () => await getById());
    }


    [Test]
    public void Calculate_Success_GetDaylyPoints()
    {
        var a = _dailyPointSrv.Calculate();

        Assert.Pass();
    }
}
