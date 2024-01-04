using DataAccess.Contexts;
using Entities.Concrete;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LibraryAPIDbContext())
            {
                // Örnek kitap oluştur
                var newBook = new Book
                {
                    BookName = "Example Book",
                    FixtureNumber = "kdsçjfgskldf",
                    ISBNNumber = "dfghdlfmkgh",
                    Interpreter = "jdngfksdfg",
                    PublishCount = 1,
                    PageCount = 1,
                    CreatedDate = DateTime.Now,
                  
                };

                // Örnek kategori oluştur
                var newCategory = new Category
                {
                    CategoryName = "Example Category"
                };

                // Kitap ve kategoriyi BookCategory tablosuna eklemek
                var newBookCategory = new BookCategory
                {
                    Book = newBook,
                    Category = newCategory
                };

                // Veritabanına eklemek
                context.BookCategories.Add(newBookCategory);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.InnerException);
                }
            }
        }
    }
}