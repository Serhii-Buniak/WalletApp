using AutoMapper;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories;

namespace WalletApp.BLL.Services.Realizations;

public class PaymentDueService : BaseEntityService, IPaymentDueService
{
    public PaymentDueService(IDataWrapper dataWrapper, IMapper mapper) : base(dataWrapper, mapper)
    {

    }

    public async Task<IEnumerable<PaymentDueReadDto>> GetAllAsync()
    {
        IEnumerable<PaymentDue> paymentDues = await Data.PaymentDues.GetAllAsync();

        var paymentDueReadDtos = Mapper.Map<IEnumerable<PaymentDueReadDto>>(paymentDues);

        return paymentDueReadDtos;
    }

    public async Task<PaymentDueReadDto> GetByIdAsync(long id)
    {
        PaymentDue? paymentDue = await Data.PaymentDues.GetByIdOrDefaultAsync(id);

        if (paymentDue == null)
        {
            throw new NotFoundException(nameof(PaymentDue), id);
        }

        var paymentDueReadDto = Mapper.Map<PaymentDueReadDto>(paymentDue);

        return paymentDueReadDto;
    }
}