using AutoMapper;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories;
using WalletApp.DAL.Repositories.Interfaces;

namespace WalletApp.BLL.Services.Realizations;

public class CardBalanceService : BaseEntityService, ICardBalanceService
{
    public CardBalanceService(IDataWrapper dataWrapper, IMapperService mapper) : base(dataWrapper, mapper)
    {

    }

    public async Task<CardBalanceReadDto> GetByIdAsync(long id)
    {
        CardBalance? cardBalance = await Data.CardBalances.GetByIdOrDefaultAsync(id);

        if (cardBalance == null)
        {
            throw new NotFoundException(nameof(CardBalance), id);
        }

        var cardBalanceReadDto = Mapper.Map<CardBalanceReadDto>(cardBalance);

        return cardBalanceReadDto;
    }

}
