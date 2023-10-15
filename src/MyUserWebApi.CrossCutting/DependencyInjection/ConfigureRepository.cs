using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyUserWebApi.Data.Context;
using MyUserWebApi.Data.Implementations;
using MyUserWebApi.Data.Repository;
using MyUserWebApi.Domain.Interfaces;
using MyUserWebApi.Domain.Repository;

namespace MyUserWebApi.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceColletion.AddScoped<IUserRepository, UserImplementation>();
        
            if(Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower())
            {
                serviceColletion.AddDbContext<AppDbContext>(
                    options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION"))
                );
            }
            else
            {
                // serviceColletion.AddDbContext<AppDbContext>(
                //     options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION"))
                // );
            }

        }
    }
}