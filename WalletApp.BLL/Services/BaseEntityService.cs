using AutoMapper;
using WalletApp.DAL.Repositories;

namespace WalletApp.BLL.Services;

public abstract class BaseEntityService
{
    protected IDataWrapper Data { get; set; }
    protected IMapper Mapper { get; set; }

    protected BaseEntityService(IDataWrapper dataWrapper, IMapper mapper)
    {
        Data  = dataWrapper;
        Mapper = mapper;
    }
}
