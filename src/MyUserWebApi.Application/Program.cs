using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MyUserWebApi.CrossCutting.DependencyInjection;
using MyUserWebApi.Domain.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);

var signingConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signingConfigurations);

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);
builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo{
        Version = "v1",
        Title = "API AspNetCore 7.0",
        Description = "Arquitetura DDD",
        TermsOfService = new Uri("http://www.testeinfo.com.br"),
        Contact = new OpenApiContact{
            Name = "Alexandre Correia",
            Email = "alex.sol.correia@gmail.com",
            Url = new Uri("http://www.testeinfo.com.br"),
        },
        License = new OpenApiLicense{
            Name = "Termo de Licença",
            Url = new Uri("http://www.testeinfo.com.br")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API AspNetCore 7.0");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
