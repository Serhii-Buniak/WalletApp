using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.BLL.Services.Realizations;
using WalletApp.BLL.Settings;
using WalletApp.DAL;
using WalletApp.DAL.Entities.Identity;
using WalletApp.DAL.Repositories;
using WalletApp.WebApi.Common.Extensions;

namespace WalletApp.WebApi;

public static class StartupExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddTransient<IUserAppManager, UserAppManager>();
        services.AddTransient<ITransactionService, TransactionService>();
        services.AddTransient<ICardBalanceService, CardBalanceService>();
        services.AddTransient<IPaymentDueService, PaymentDueService>();
        services.AddTransient<IDailyPointService, DailyPointService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthService, AuthService>();

        return services;
    }        
    
    public static IServiceCollection AddAppRepositories(this IServiceCollection services)
    {
        services.AddTransient<IDataWrapper, DataWrapper>();

        return services;
    }    
    
    public static IServiceCollection AddAppConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        return services;
    }

    public static void AddAppDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        string? connection = configuration.GetConnectionString("NpgDatabase")
            ?? throw new NullReferenceException("NpgDatabase is missing");

        services.AddDbContext<AppDbContext>(o => o.UseNpgsql(connection));
    }

    public static void AddAppIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>(opt =>
        {
            opt.User.RequireUniqueEmail = false;
            opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
            opt.Password.RequiredLength = 0;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireDigit = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireLowercase = false;

        }).AddEntityFrameworkStores<AppDbContext>();

    }

    public static void AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });
    }

    public static void AddAppSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

    }

    public static void AddAppValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAppAssemblies());
    }

    public static void AddAppMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAppAssemblies());
    }
}
