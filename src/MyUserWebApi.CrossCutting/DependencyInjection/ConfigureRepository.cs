using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyUserWebApi.Data.Context;
using MyUserWebApi.Data.Repository;
using MyUserWebApi.Domain.Interfaces;

namespace MyUserWebApi.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        
            serviceColletion.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=localhost,1450;Initial Catalog=MyApi;MultipleActiveResultSets=true;User ID=SA;Password=Numsey#2022"));

        }
    }
}