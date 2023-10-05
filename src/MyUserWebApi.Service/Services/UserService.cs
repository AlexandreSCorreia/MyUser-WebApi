using MyUserWebApi.Domain.Entities;
using MyUserWebApi.Domain.Interfaces;
using MyUserWebApi.Domain.Interfaces.Services.User;

namespace MyUserWebApi.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _repository;

        public UserService(IRepository<UserEntity> repository)
        {
            this._repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await this._repository.DeleteAsync(id);
        }

        public async Task<UserEntity> Get(Guid id)
        {
            return await this._repository.SelectAsync(id);
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await this._repository.SelectAsync();
        }

        public async Task<UserEntity> Post(UserEntity user)
        {
            return await this._repository.InsertAsync(user);
        }

        public async Task<UserEntity> Put(UserEntity user)
        {
            return await this._repository.UpdateAsync(user);
        }
    }
}