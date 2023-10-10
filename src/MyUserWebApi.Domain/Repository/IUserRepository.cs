using MyUserWebApi.Domain.Entities;
using MyUserWebApi.Domain.Interfaces;

namespace MyUserWebApi.Domain.Repository
{
    public interface IUserRepository: IRepository<UserEntity>
    {
        Task<UserEntity> FindByLogin (string email);
    }
}