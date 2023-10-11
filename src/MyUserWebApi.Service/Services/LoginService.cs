using MyUserWebApi.Domain.Entities;
using MyUserWebApi.Domain.Interfaces.Services.User;
using MyUserWebApi.Domain.Repository;

namespace MyUserWebApi.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;

        public LoginService(IUserRepository repository)
        {
            this._repository = repository;
        }

        public async Task<object> FindByLogin(UserEntity user)
        {
            if(user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
               return await _repository.FindByLogin(user.Email);
            }
            else
            {
                return null;
            }     
        }
    }
}