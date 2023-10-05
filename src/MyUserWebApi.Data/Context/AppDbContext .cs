using Microsoft.EntityFrameworkCore;
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
        }

    }
}