using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Dtos.PaymentDueDtos;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Common.Pagination;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Identity;
using WalletApp.DAL.Repositories;

namespace WalletApp.BLL.Services.Realizations;

public class TransactionService : BaseEntityService, ITransactionService
{

    public TransactionService(IDataWrapper dataWrapper, IMapperService mapper) : base(dataWrapper, mapper)
    {

    }

    public async Task<TransactionReadDto> AddAsync(TransactionAddDto transactionAdd)
    {
        bool isUserExist = await Data.Users.ContainsByIdAsync(transactionAdd.UserId);
        if (!isUserExist)
        {
            throw new NotFoundException(nameof(AppUser), transactionAdd.UserId);
        }

        if (transactionAdd.SenderId != null)
        {
            bool isSenderExist = await Data.Users.ContainsByIdAsync(transactionAdd.SenderId.Value);
            if (!isSenderExist)
            {
                throw new NotFoundException(nameof(AppUser), transactionAdd.SenderId);
            }
        }

        var transaction = Mapper.Map<Transaction>(transactionAdd);

        transaction = await Data.Transactions.CreateAsync(transaction);

        await Data.SaveAsync();

        var transactionReadDto = Mapper.TransactionEntityToReadDto(transaction);

        return transactionReadDto;
    }

    public async Task<TransactionReadDto> GetByIdAsync(long id)
    {
        Transaction? transaction =
            await Data.Transactions.GetByIdOrDefaultAsync(id,
            include: i => i.Include(p => p.Sender!)
            );;

        if (transaction == null)
        {
            throw new NotFoundException(nameof(Transaction), id);
        }

        var transactionReadDto = Mapper.TransactionEntityToReadDto(transaction);

        return transactionReadDto;
    }

}
