using AutoMapper;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.BLL.ValueConverters;
using WalletApp.DAL.Entities;

namespace WalletApp.BLL.Services.Realizations;

public class MapperService : Mapper, IMapperService
{
    private readonly IDateTimeService _dateTimeSrv;

    public MapperService(IConfigurationProvider configuration, IDateTimeService dateTimeService) : base(configuration)
    {
        _dateTimeSrv = dateTimeService;
    }

    public TransactionReadDto TransactionEntityToReadDto(Transaction transaction)
    {
        var transactionReadDto = Map<TransactionReadDto>(transaction);

        transactionReadDto.WasCreated = new DateTimeToStringConverter().Convert(transaction.CreatedAt, _dateTimeSrv.Now);

        return transactionReadDto;
    }

    public IEnumerable<TransactionReadDto> TransactionsEntityToReadDtos(IEnumerable<Transaction> transactions)
    {
        var converter = new DateTimeToStringConverter();
        DateTime now = _dateTimeSrv.Now;


        return transactions.Select((t) =>
        {
            var transactionReadDto = Map<TransactionReadDto>(t);
            transactionReadDto.WasCreated = converter.Convert(t.CreatedAt, now);
            return transactionReadDto;
        });
    }
}

