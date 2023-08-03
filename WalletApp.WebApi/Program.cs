using Npgsql;
using WalletApp.WebApi;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAppDatabase(configuration);

builder.Services.AddAppIdentity();
builder.Services.AddAppAuthentication(configuration);

builder.Services.AddAppConfigureSettings(configuration);

builder.Services.AddAppSwaggerGen();

builder.Services.AddAppValidation();
builder.Services.AddAppMapping();

builder.Services.AddAppServices();
builder.Services.AddAppRepositories();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

