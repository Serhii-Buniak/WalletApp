using Microsoft.EntityFrameworkCore.Query;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.DAL.Repositories.Interfaces;

public interface ICardBalanceRepository
{
    Task<CardBalance?> GetByIdOrDefaultAsync(long id);
    Task<IEnumerable<CardBalance>> GetAllAsync();
}
