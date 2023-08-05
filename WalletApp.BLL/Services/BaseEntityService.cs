using AutoMapper;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.DAL.Repositories;

namespace WalletApp.BLL.Services;

public abstract class BaseEntityService
{
    protected IDataWrapper Data { get; set; }
    protected IMapperService Mapper { get; set; }

    protected BaseEntityService(IDataWrapper dataWrapper, IMapperService mapper)
    {
        Data  = dataWrapper;
        Mapper = mapper;
    }
}
