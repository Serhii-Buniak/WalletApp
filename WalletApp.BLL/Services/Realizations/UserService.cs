using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WalletApp.BLL.Dtos.CardBalanceDtos;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Dtos.UserDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Identity;
using WalletApp.DAL.Repositories;

namespace WalletApp.BLL.Services.Realizations;

public class UserService : BaseEntityService, IUserService
{
    public UserService(IDataWrapper dataWrapper, IMapper mapper) : base(dataWrapper, mapper)
    {
    }

    public async Task<UserReadDto> GetByIdAsync(Guid id)
    {
        AppUser? appUser = await Data.Users.GetByIdOrDefaultAsync(id);

        if (appUser == null)
        {
            throw new NotFoundException(nameof(AppUser), id);
        }

        var userReadDto = Mapper.Map<UserReadDto>(appUser);

        return userReadDto;
    }

    public async Task<CardBalanceReadDto> GetCardBalanceReadDtoAsync(Guid userId)
    {
        AppUser? appUser = await Data.Users.GetByIdOrDefaultAsync(userId,
            include: i => i.Include(user => user.CardBalance)
            );

        if (appUser == null)
        {
            throw new NotFoundException(nameof(AppUser), userId);
        }

        var cardBalanceRead = Mapper.Map<CardBalanceReadDto>(appUser.CardBalance);

        return cardBalanceRead;
    }

    public async Task<IEnumerable<UserReadDto>> GetAllAsync()
    {
        IEnumerable<AppUser> appUsers = await Data.Users.GetAllAsync();

        var userReadDtos = Mapper.Map<IEnumerable<UserReadDto>>(appUsers);

        return userReadDtos;
    }

    public async Task<PaymentDueReadDto> GetPaymentDueReadDtoAsync(Guid userId)
    {
        AppUser? appUser = await Data.Users.GetByIdOrDefaultAsync(userId,
            include: i => i.Include(user => user.PaymentDue)
        );

        if (appUser == null)
        {
            throw new NotFoundException(nameof(AppUser), userId);
        }

        var paymentDueReadDto = Mapper.Map<PaymentDueReadDto>(appUser.PaymentDue);

        return paymentDueReadDto;
    }

    public async Task<DailyPointReadDto> GetDailyPointReadDtoAsync(Guid userId)
    {
        AppUser? appUser = await Data.Users.GetByIdOrDefaultAsync(userId,
                 include: i => i.Include(user => user.DailyPoint)
             );

        if (appUser == null)
        {
            throw new NotFoundException(nameof(AppUser), userId);
        }

        var dailyPointReadDto = Mapper.Map<DailyPointReadDto>(appUser.DailyPoint);

        return dailyPointReadDto;
    }
}