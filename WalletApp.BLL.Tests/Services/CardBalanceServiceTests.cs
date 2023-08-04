using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WalletApp.BLL.Dtos.AuthDtos;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Services.Realizations;
using WalletApp.BLL.Settings;
using WalletApp.Common.Exceptions;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Identity;
using WalletApp.DAL.Repositories;
using WalletApp.Tests.Helpers;
using WalletApp.Tests.TestHelpers;

namespace WalletApp.BLL.Tests.Services;

internal class CardBalanceServiceTests
{
    private Mock<IMapper> _mapper;
    private Mock<IDataWrapper> _dataWrapper;

    private CardBalanceService _cardBalanceSrv;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mock<IMapper>();
        _dataWrapper = new Mock<IDataWrapper>();

        _cardBalanceSrv = new CardBalanceService(_dataWrapper.Object, _mapper.Object);
    }

    [Test]
    public async Task GetByIdAsync_Success_ReturnCardBalanceReadDto()
    {
        _dataWrapper
            .Setup(d => d.CardBalances.GetByIdOrDefaultAsync(It.IsAny<long>()))
            .ReturnsAsync(CardBalanceTestHelper.GetCardBalance());

        _mapper
            .Setup(m => m.Map<CardBalanceReadDto>(It.IsAny<CardBalance>()))
            .Returns(CardBalanceTestHelper.GetCardBalanceReadDto());

        CardBalanceReadDto cardBalanceReadDto = await _cardBalanceSrv.GetByIdAsync(It.IsAny<long>());

        Assert.That(cardBalanceReadDto.Id, Is.EqualTo(CardBalanceTestHelper.GetCardBalance().Id));
    }

    [Test]
    public void GetByIdAsync_NotFound_ThrowNotFoundException()
    {
        _dataWrapper
            .Setup(d => d.CardBalances.GetByIdOrDefaultAsync(It.IsAny<long>()))
            .ThrowsAsync(new NotFoundException());

        _mapper
            .Setup(m => m.Map<CardBalanceReadDto>(It.IsAny<CardBalance>()))
            .Returns(CardBalanceTestHelper.GetCardBalanceReadDto());

        var getById = async () => await _cardBalanceSrv.GetByIdAsync(It.IsAny<long>());

        Assert.ThrowsAsync<NotFoundException>(async () => await getById());
    }
}
