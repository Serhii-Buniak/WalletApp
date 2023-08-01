using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.DAL;

namespace WalletApp.WebApi;

public static class StartupExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddTransient<ITransactionService, TransactionService>();

        return services;
    }

    public static void AddAppDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        string? connection = configuration.GetConnectionString("NpgDatabase") 
            ?? throw new NullReferenceException("NpgDatabase is missing");

        services.AddDbContext<AppDbContext>(o
                => o.UseNpgsql(connection));
    }
    
    public static void AddAutoValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }    
    
    public static void AddAutoMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
