using Core.Configuration;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Contexts
{
    public class LibraryAPIDbContext : DbContext
    {
        string connectionString = DbConfiguration.ConnectionString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Language> Languages { get; set; }
    }
}
