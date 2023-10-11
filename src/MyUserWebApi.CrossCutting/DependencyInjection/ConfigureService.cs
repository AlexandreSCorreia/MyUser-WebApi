using Microsoft.Extensions.DependencyInjection;
using MyUserWebApi.Domain.Interfaces.Services.User;
using MyUserWebApi.Service.Services;

namespace MyUserWebApi.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceColletion)
        {
            serviceColletion.AddTransient<IUserService, UserService>();
            serviceColletion.AddTransient<ILoginService, LoginService>();
        }
    }
}