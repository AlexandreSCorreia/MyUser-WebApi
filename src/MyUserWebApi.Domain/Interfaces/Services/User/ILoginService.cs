
using MyUserWebApi.Domain.Entities;

namespace MyUserWebApi.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<Object> FindByLogin(UserEntity user);
    }
}