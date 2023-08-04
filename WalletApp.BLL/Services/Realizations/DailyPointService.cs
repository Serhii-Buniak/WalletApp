using AutoMapper;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories;

namespace WalletApp.BLL.Services.Realizations;

public class DailyPointService : BaseEntityService, IDailyPointService
{
    public DailyPointService(IDataWrapper dataWrapper, IMapper mapper) : base(dataWrapper, mapper)
    {

    }

    public async Task<DailyPointReadDto> GetByIdAsync(long id)
    {
        DailyPoint? dailyPoint = await Data.DailyPoints.GetByIdOrDefaultAsync(id);

        if (dailyPoint == null)
        {
            throw new NotFoundException(nameof(DailyPoint), id);
        }

        var dailyPointReadDto = Mapper.Map<DailyPointReadDto>(dailyPoint);

        return dailyPointReadDto;
    }
}