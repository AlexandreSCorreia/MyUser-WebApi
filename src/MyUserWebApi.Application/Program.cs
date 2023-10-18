using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MyUserWebApi.CrossCutting.DependencyInjection;
using MyUserWebApi.CrossCutting.Mappings;
using MyUserWebApi.Data.Context;
using MyUserWebApi.Domain.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToModelProfile());
    cfg.AddProfile(new EntityToDtoProfile());
    cfg.AddProfile(new ModelToEntityProfile());
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var signingConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signingConfigurations);

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);
builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
}).AddJwtBearer(bearerOptions => 
{
    var paramsValidation = bearerOptions.TokenValidationParameters;
    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
    paramsValidation.ValidAudience = tokenConfigurations.Audience;
    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
    paramsValidation.ValidateIssuerSigningKey = true;
    paramsValidation.ValidateLifetime = true;
    paramsValidation.ClockSkew = TimeSpan.Zero;
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
});

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

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Entre com o Token JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<String>()
        }
    });
});

var app = builder.Build();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
		
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

if (Environment.GetEnvironmentVariable("MIGRATION").ToLower() == "APLICAR".ToLower())
{
    using (var service = app.Services.GetRequiredService<IServiceScopeFactory>()
                                    .CreateScope())
    {
        using (var context = service.ServiceProvider.GetService<AppDbContext>())
        {
            context.Database.Migrate();
        }
    }
}

app.Run();
