using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class LibraryAPIDbContext : DbContext
    {
        public LibraryAPIDbContext(DbContextOptions<LibraryAPIDbContext> options) : base(options)
        {
        }

        public LibraryAPIDbContext()
        {
        }

        //string connectionString = "Server=94.73.147.32;User=u0756268_user16;Password=ASD123_Asd123_asd123;database=u0756268_dblms";    //DbConfiguration.ConnectionString;
        string connectionString = "Server=94.73.147.32;User=u0756268_test13;Password=-qm3LN.85.0eW@Pf;database=u0756268_test13";    //DbConfiguration.ConnectionString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Interpreter> Interpreters { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<DepositBook> DepositBooks { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
