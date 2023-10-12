using MyUserWebApi.Domain.Dtos;

namespace MyUserWebApi.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<Object> FindByLogin(LoginDto user);
    }
}