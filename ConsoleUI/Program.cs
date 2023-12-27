using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            void Add(Language language)
            {
                using (LibraryAPIDbContext context = new LibraryAPIDbContext())
                {
                    var addedEntity = context.Entry(language);
                    addedEntity.State = EntityState.Added;
                    context.SaveChanges();
                }
            }

            Language lang1 = new Language() { LanguageName = "Urduca" };
            Add(lang1);
        }
    }
}
