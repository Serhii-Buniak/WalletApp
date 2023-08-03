using System.Reflection;
using System.Runtime.CompilerServices;

namespace WalletApp.WebApi.Common.Extensions;

public static class AppDomainExtensions
{
    public static IEnumerable<Assembly> GetAppAssemblies(this AppDomain appDomain)
    {
        return appDomain.GetAssemblies().Where(assem => assem.GetName().FullName.Contains(nameof(WalletApp)));
    }
}
