using Core.Entities.Concrete;
using Core.Entities.Concrete.enums;
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
        public DbSet<Interpreter> Interpreters { get; set; }
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


            Guid language1Guid = Guid.Parse("f72c1123-5065-4e3c-aa99-a53c004e05cf");
            Guid language2Guid = Guid.Parse("15857832-efe5-4efc-a9a9-7cf8c2374858");
            Guid language3Guid = Guid.Parse("1cc01062-f1d0-413c-90f6-e1b33d340e78");
            Guid language4Guid = Guid.Parse("637a6eec-508c-4358-ab20-78c82e7a0601");
            Guid language5Guid = Guid.Parse("f8937ba1-4d7a-44fd-aed9-bb2860bf8132");

            modelBuilder.Entity<Language>().HasData(new Language
            {
                LanguageId = language1Guid,
                LanguageName = "Türkçe"
            });

            modelBuilder.Entity<Language>().HasData(new Language
            {
                LanguageId = language2Guid,
                LanguageName = "Danca"
            });

            modelBuilder.Entity<Language>().HasData(new Language
            {
                LanguageId = language3Guid,
                LanguageName = "İngilizce"
            });

            modelBuilder.Entity<Language>().HasData(new Language
            {
                LanguageId = language4Guid,
                LanguageName = "Almanca"
            });

            modelBuilder.Entity<Language>().HasData(new Language
            {
                LanguageId = language5Guid,
                LanguageName = "Arapça"
            });

            Guid publisher1Guid = Guid.Parse("914cf986-bc1b-4e3f-a674-3e4f2a71e38b");
            Guid publisher2Guid = Guid.Parse("69daac5d-30e0-4f57-8d6b-664a047c160c");
            Guid publisher3Guid = Guid.Parse("ca7ee10f-a5f7-4339-82ff-3f661fc4dcf6");
            Guid publisher4Guid = Guid.Parse("be85ed7e-74f7-421a-bdf2-8a792b8fcfb2");
            Guid publisher5Guid = Guid.Parse("a33642e6-386c-4375-ad1b-790c51da3e14");


            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                PublisherId = publisher1Guid,
                PublisherName = "Ithaki"
            });

            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                PublisherId = publisher2Guid,
                PublisherName = "Kültür"
            });

            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                PublisherId = publisher3Guid,
                PublisherName = "Mecaz"
            });

            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                PublisherId = publisher4Guid,
                PublisherName = "Can"
            });

            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                PublisherId = publisher5Guid,
                PublisherName = "İş Bankası"
            });

            Guid category1Guid = Guid.Parse("1a0e88d2-2d26-41b6-93b1-735d468ca181");
            Guid category2Guid = Guid.Parse("e52bb2b5-cffd-4822-8901-474db975313e");
            Guid category3Guid = Guid.Parse("c140fe74-88d7-4b49-8dfa-7eca685c58a4");
            Guid category4Guid = Guid.Parse("1c324b42-280c-4ff4-b051-22673a8117aa");
            Guid category5Guid = Guid.Parse("c70c610e-f442-4e6c-ad2d-55940e4f9ea2");

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = category1Guid,
                CategoryName = "Şiir"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = category2Guid,
                CategoryName = "Hikaye"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = category3Guid,
                CategoryName = "Roman"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = category4Guid,
                CategoryName = "Deneme"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = category5Guid,
                CategoryName = "Anı"
            });

            Guid interpreter1Guid = Guid.Parse("d7163905-df9b-4ed6-a426-ab1d09fba731");
            Guid interpreter2Guid = Guid.Parse("366243fd-e87c-422f-bea6-050d6b9016af");
            Guid interpreter3Guid = Guid.Parse("7653730d-4b8e-4b71-a9f6-55e6772ee808");
            Guid interpreter4Guid = Guid.Parse("6d36c961-649f-47fa-955c-142e60121aa1");
            Guid interpreter5Guid = Guid.Parse("5b908681-6f3b-4463-928f-40de9c9ff054");

            modelBuilder.Entity<Interpreter>().HasData(new Interpreter
            {
                InterpreterId = interpreter1Guid,
                InterpreterFirstName = "Mustafa",
                InterpreterLastName = "Kaya"
            });

            modelBuilder.Entity<Interpreter>().HasData(new Interpreter
            {
                InterpreterId = interpreter2Guid,
                InterpreterFirstName = "Halil Eren",
                InterpreterLastName = "Yazıcı"
            });

            modelBuilder.Entity<Interpreter>().HasData(new Interpreter
            {
                InterpreterId = interpreter3Guid,
                InterpreterFirstName = "Kadir",
                InterpreterLastName = "Korkmaz"
            });

            modelBuilder.Entity<Interpreter>().HasData(new Interpreter
            {
                InterpreterId = interpreter4Guid,
                InterpreterFirstName = "Baha",
                InterpreterLastName = "Can"
            });

            modelBuilder.Entity<Interpreter>().HasData(new Interpreter
            {
                InterpreterId = interpreter5Guid,
                InterpreterFirstName = "Berkay",
                InterpreterLastName = "Gürcan"
            });

            Guid location1Guid = Guid.Parse("ed1dd8b8-4186-4a53-a847-7d8e6943dd85");
            Guid location2Guid = Guid.Parse("a6161ff7-0ba4-48bd-bed5-6004fa894265");
            Guid location3Guid = Guid.Parse("5a70e086-d809-435f-9cf1-9fd4afbde054");
            Guid location4Guid = Guid.Parse("3a1c8bd9-578e-408f-a7b7-7d4a0c302a07");
            Guid location5Guid = Guid.Parse("49e649bc-0f72-4ca5-9283-d4eef26b7a0f");

            modelBuilder.Entity<Location>().HasData(new Location
            {
                LocationId = location1Guid,
                Shelf = "A1"
            });

            modelBuilder.Entity<Location>().HasData(new Location
            {
                LocationId = location2Guid,
                Shelf = "A2"
            });

            modelBuilder.Entity<Location>().HasData(new Location
            {
                LocationId = location3Guid,
                Shelf = "B1"
            });

            modelBuilder.Entity<Location>().HasData(new Location
            {
                LocationId = location4Guid,
                Shelf = "B2"
            });

            modelBuilder.Entity<Location>().HasData(new Location
            {
                LocationId = location5Guid,
                Shelf = "C1"
            });

            Guid department1Guid = Guid.Parse("6dcf2e63-05cf-4cb0-a18b-9c54e67fc9c0");
            Guid department2Guid = Guid.Parse("1e64027b-4650-4338-9b2e-a4707b42750c");
            Guid department3Guid = Guid.Parse("32ce91c7-e989-4942-af99-04c69bac8cf4");
            Guid department4Guid = Guid.Parse("6f64cfb0-750d-43d6-948d-eb2c90b3d6ac");
            Guid department5Guid = Guid.Parse("9ee6b64d-094b-4a9e-bf91-92d80f8041d5");

            modelBuilder.Entity<Department>().HasData(new Department
            {
                DepartmentId = department1Guid,
                DepartmentName = "YBS"
            });

            modelBuilder.Entity<Department>().HasData(new Department
            {
                DepartmentId = department2Guid,
                DepartmentName = "UTI"
            });

            modelBuilder.Entity<Department>().HasData(new Department
            {
                DepartmentId = department3Guid,
                DepartmentName = "İşletme"
            });

            modelBuilder.Entity<Department>().HasData(new Department
            {
                DepartmentId = department4Guid,
                DepartmentName = "Çocuk Gelişimi"
            });

            modelBuilder.Entity<Department>().HasData(new Department
            {
                DepartmentId = department5Guid,
                DepartmentName = "Uluslararası İlişkiler"
            });

            Guid faculty1Guid = Guid.Parse("f28929d4-7164-44be-8dd4-d4474f8e458f");
            Guid faculty2Guid = Guid.Parse("13a57a98-5aeb-4d20-ab75-673260be7a5f");
            Guid faculty3Guid = Guid.Parse("2307fe55-e881-4718-a1ea-ea18ede54aea");
            Guid faculty4Guid = Guid.Parse("7e1422de-b065-4f41-846d-96fe5d932c2f");
            Guid faculty5Guid = Guid.Parse("deb213e7-e519-4a46-850b-e602c3570c33");

            modelBuilder.Entity<Faculty>().HasData(new Faculty
            {
                FacultyId = faculty1Guid,
                FacultyName = "INIF"
            });

            modelBuilder.Entity<Faculty>().HasData(new Faculty
            {
                FacultyId = faculty2Guid,
                FacultyName = "Eğitim"
            });

            modelBuilder.Entity<Faculty>().HasData(new Faculty
            {
                FacultyId = faculty3Guid,
                FacultyName = "MYO"
            });

            modelBuilder.Entity<Faculty>().HasData(new Faculty
            {
                FacultyId = faculty4Guid,
                FacultyName = "İİBF"
            });

            modelBuilder.Entity<Faculty>().HasData(new Faculty
            {
                FacultyId = faculty5Guid,
                FacultyName = "Fen Edebiyat"
            });

            //Guid user1Guid = Guid.Parse("f28929d4-7164-44be-8dd4-d4474f8e458f");
            //Guid user2Guid = Guid.Parse("13a57a98-5aeb-4d20-ab75-673260be7a5f");

            //modelBuilder.Entity<User>().HasData(new User
            //{
            //    UserId = user1Guid,
            //    FirstName = "Şenol",
            //    LastName = "Şen",
            //    Email = "132030026@ogr.uludag.edu.tr",
            //    PhoneNumber = "1234567890",
            //    SchoolNumber = "132030026",
            //    RoleType = RoleType.STUDENT
            //});



        }
    }
}
