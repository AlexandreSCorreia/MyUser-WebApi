using Microsoft.EntityFrameworkCore;
using MyUserWebApi.Data.Context;
using MyUserWebApi.Data.Repository;
using MyUserWebApi.Domain.Entities;
using MyUserWebApi.Domain.Repository;

namespace MyUserWebApi.Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataset;

        public UserImplementation(AppDbContext context) : base(context)
        {
            _dataset = context.Set<UserEntity>();
        }

        public async Task<UserEntity> FindByLogin(string email)
        {
           return await _dataset.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}