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
    private DailyPointService _dailyPointSrv;

    [SetUp]
    public void Setup()
    {
        _dailyPointSrv = new DailyPointService();
    }


    [Test]
    public void Calculate_Success_GetDaylyPoints()
    {
        var a = _dailyPointSrv.Get();

        Assert.Pass();
    }
}
