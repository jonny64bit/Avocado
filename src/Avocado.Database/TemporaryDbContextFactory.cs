using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Avocado.Database
{
    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<DAL>
    {
        public DAL CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DAL>();
            builder.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(nameof(Database)));

            return new DAL(builder.Options);
        }
    }
}