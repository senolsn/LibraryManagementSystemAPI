using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;

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
        //string connectionString = "Server=94.73.147.32;User=u0756268_testdbl;Password=Xb1_4R9UraH.3x::;database=u0756268_testdbl";    //DbConfiguration.ConnectionString;
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
        public DbSet<Book> Books { get; set; }
        public DbSet<DepositBook> DepositBooks { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid author1Guid = Guid.Parse("7d5d9918-8894-47cd-b151-7568cbdcb59f");
            Guid author2Guid = Guid.Parse("7374acef-7c4a-4d7c-ad49-3fd5665f2c64");
            Guid author3Guid = Guid.Parse("669ee76d-5f87-4d14-b5c2-f4475f0b8d0c");
            Guid author4Guid = Guid.Parse("57deceaa-5ebf-412b-9eb9-22ad3588f202");
            Guid author5Guid = Guid.Parse("895c97d6-5a15-4819-9fb1-42f125803562");

            modelBuilder.Entity<Author>().HasData(
             new Author
                {
                    AuthorId = author1Guid,
                    AuthorFirstName = "John",
                    AuthorLastName = "Doe"
                 });

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    AuthorId = author2Guid,
                    AuthorFirstName = "Jane",
                    AuthorLastName = "Doe"
                });

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    AuthorId = author3Guid,
                    AuthorFirstName = "Michael",
                    AuthorLastName = "Doe"
                });

            modelBuilder.Entity<Author>().HasData(
               new Author
               {
                   AuthorId = author4Guid,
                   AuthorFirstName = "Justin",
                   AuthorLastName = "Doe"
               });

            modelBuilder.Entity<Author>().HasData(
               new Author
               {
                   AuthorId = author5Guid,
                   AuthorFirstName = "Domanic",
                   AuthorLastName = "Campbell"
               });


            Guid language1Guid = Guid.Parse("70660874-271f-4c35-af1c-7a363df319af");
            Guid language2Guid = Guid.Parse("70660874-271f-4c35-af1c-7a363df319af");
            Guid language3Guid = Guid.Parse("70660874-271f-4c35-af1c-7a363df319af");
            Guid language4Guid = Guid.Parse("70660874-271f-4c35-af1c-7a363df319af");
            Guid language5Guid = Guid.Parse("70660874-271f-4c35-af1c-7a363df319af");

            modelBuilder.Entity<Language>().HasData(new Language
            {
                LanguageId = Guid.NewGuid(),
                LanguageName = "Türkçe"
            });

            modelBuilder.Entity<Language>().HasData(new Language
            {
                LanguageId = Guid.NewGuid(),
                LanguageName = "Danca"
            });

            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                PublisherId = Guid.NewGuid(),
                PublisherName = "Abc"
            });

            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                PublisherId = Guid.NewGuid(),
                PublisherName = "Bca"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "Category"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "Category"
            });
        }
    }
}
