using Microsoft.EntityFrameworkCore;
using MyUserWebApi.Data.Mapping;
using MyUserWebApi.Domain.Entities;

namespace MyUserWebApi.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>( new UserMap().Configure);
            
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrador",
                    SurName = "",
                    Email = "admininfo@mail.com",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                }
            );
        }

    }
}