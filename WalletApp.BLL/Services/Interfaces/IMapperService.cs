using AutoMapper;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.DAL.Entities;

namespace WalletApp.BLL.Services.Interfaces;

public interface IMapperService : IMapper
{
    TransactionReadDto TransactionEntityToReadDto(Transaction transaction);
    IEnumerable<TransactionReadDto> TransactionsEntityToReadDtos(IEnumerable<Transaction> transactions);
}