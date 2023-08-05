using Moq;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.BLL.Services.Realizations;
using WalletApp.Common.Exceptions;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories;
using WalletApp.Tests.Helpers;

namespace WalletApp.BLL.Tests.Services;

internal class PaymentDueServiceTests
{
    private Mock<IMapperService> _mapper;
    private Mock<IDataWrapper> _dataWrapper;

    private PaymentDueService _paymentDueSrv;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mock<IMapperService>();
        _dataWrapper = new Mock<IDataWrapper>();

        _paymentDueSrv = new PaymentDueService(_dataWrapper.Object, _mapper.Object);
    }

    [Test]
    public async Task GetByIdAsync_Success_ReturnPaymentDueReadDto()
    {
        _dataWrapper
            .Setup(d => d.PaymentDues.GetByIdOrDefaultAsync(It.IsAny<long>()))
            .ReturnsAsync(PaymentDueTestHelper.GetPaymentDue());

        _mapper
            .Setup(m => m.Map<PaymentDueReadDto>(It.IsAny<PaymentDue>()))
            .Returns(PaymentDueTestHelper.GetPaymentDueReadDto());

        var paymentDueReadDto= await _paymentDueSrv.GetByIdAsync(It.IsAny<long>());

        Assert.That(paymentDueReadDto.Id, Is.EqualTo(PaymentDueTestHelper.GetPaymentDue().Id));
    }

    [Test]
    public void GetByIdAsync_NotFound_ThrowNotFoundException()
    {
        _dataWrapper
            .Setup(d => d.PaymentDues.GetByIdOrDefaultAsync(It.IsAny<long>()))
            .ThrowsAsync(new NotFoundException());

        _mapper
            .Setup(m => m.Map<PaymentDueReadDto>(It.IsAny<PaymentDue>()))
            .Returns(PaymentDueTestHelper.GetPaymentDueReadDto());

        var getById = async () => await _paymentDueSrv.GetByIdAsync(It.IsAny<long>());

        Assert.ThrowsAsync<NotFoundException>(async () => await getById());
    }
}
