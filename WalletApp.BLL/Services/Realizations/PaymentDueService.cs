using AutoMapper;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories;

namespace WalletApp.BLL.Services.Realizations;

public class PaymentDueService : BaseEntityService, IPaymentDueService
{
    public PaymentDueService(IDataWrapper dataWrapper, IMapperService mapper) : base(dataWrapper, mapper)
    {

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