using System.Reflection;
using AutoMapper;
using AutoMapper.Internal;
using WalletApp.WebApi.Common.Extensions;

namespace WalletApp.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAppAssemblies())
        {
            ApplyMappingsFromAssembly(assembly);
        }
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        Type simpleMapFromType = typeof(IMap);
        List<Type> genericMapsFromTypes = new()
        {
            typeof(IMapFrom<>),
            typeof(IMapTo<>),
            typeof(IMapFromAndTo<>),
        };

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t) => t == simpleMapFromType || 
            t.IsGenericType &&  genericMapsFromTypes.Contains(t.GetGenericTypeDefinition());

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
