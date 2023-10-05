using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyUserWebApi.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost,1450;Initial Catalog=MyApi;MultipleActiveResultSets=true;User ID=SA;Password=Numsey#2022";
            var OptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            OptionsBuilder.UseSqlServer(connectionString);
            return new AppDbContext (OptionsBuilder.Options);
        }
    }
}