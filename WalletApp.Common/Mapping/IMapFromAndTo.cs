using AutoMapper;

namespace WalletApp.Common.Mapping;

public interface IMapFromAndTo<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
}

