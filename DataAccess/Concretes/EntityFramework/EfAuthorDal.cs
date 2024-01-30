using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfAuthorDal : EfRepositoryBase<Author, LibraryAPIDbContext>, IAuthorDal
    {
        public EfAuthorDal(LibraryAPIDbContext context) : base(context)
        {
        }

        public async override Task<Author?> GetAsync(Expression<Func<Author, bool>> predicate, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            // Veritabanındaki kategorileri sorgulamak için IQueryable bir sorgu oluşturuyoruz.
            var queryable = base.Context.Authors.AsQueryable();

            // Oluşturulan sorguya ilişkili verilerin eklenmesi işlemi gerçekleşiyor.
            // Her bir kategori nesnesinin CategoryBooks koleksiyonunu ve her CategoryBook nesnesinin BookCategories koleksiyonunu dahil ediyoruz.
            queryable = queryable.Include(x => x.AuthorBooks).ThenInclude(x => x.BookAuthors); //Category ve Book Arasında Bir İlişki Varsa İkisinide Sorguya dahil et.


            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
