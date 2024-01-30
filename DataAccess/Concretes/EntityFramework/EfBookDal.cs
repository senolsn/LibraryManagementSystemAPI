using Core.DataAccess.Paging;
using Core.DataAccess.Repositories;
using Core.Entities.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfBookDal : EfRepositoryBase<Book, LibraryAPIDbContext>, IAsyncRepository<Book>, IBookDal
    {
        private readonly LibraryAPIDbContext _context;
        public EfBookDal(LibraryAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async override Task<Book> AddAsync(Book entity)
        {

            entity.CreatedDate = DateTime.Now;
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async override Task<ICollection<Book>> GetListAsync(Expression<Func<Book, bool>> predicate, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Books.AsQueryable();

            queryable = queryable.Include(x => x.BookAuthors).ThenInclude(x => x.AuthorBooks);
            queryable = queryable.Include(x => x.BookCategories).ThenInclude(x => x.CategoryBooks);
            queryable = queryable.Include(x => x.BookLanguages).ThenInclude(x => x.LanguageBooks);
            queryable = queryable.Include(x => x.BookInterpreters).ThenInclude(x => x.InterpreterBooks);
            queryable = queryable.Include(x => x.BookInterpreters).ThenInclude(x => x.InterpreterBooks);
            queryable = queryable.Include(p => p.Publisher);
            queryable = queryable.Include(p => p.Location);


            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters().Where(field => field.DeletedDate == null); //Girdiğim filter koşulunu sağlayan verileri topla.
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async override Task<Book?> GetAsync(Expression<Func<Book, bool>> predicate, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Books.AsQueryable();

            queryable = queryable.Include(x => x.BookAuthors).ThenInclude(x => x.AuthorBooks);
            queryable = queryable.Include(x => x.BookCategories).ThenInclude(x => x.CategoryBooks);
            queryable = queryable.Include(x => x.BookLanguages).ThenInclude(x => x.LanguageBooks);
            queryable = queryable.Include(x => x.BookInterpreters).ThenInclude(x => x.InterpreterBooks);
            queryable = queryable.Include(x => x.BookInterpreters).ThenInclude(x => x.InterpreterBooks);
            queryable = queryable.Include(p => p.Publisher);
            queryable = queryable.Include(p => p.Location);


            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters().Where(field => field.DeletedDate == null); //Girdiğim filter koşulunu sağlayan verileri topla.
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.FirstOrDefaultAsync(cancellationToken);
        }

        public async override Task<IPaginate<Book>> GetPaginatedListAsync(Expression<Func<Book, bool>> predicate, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Books.AsQueryable();

            queryable = queryable.Include(x => x.BookAuthors).ThenInclude(x => x.AuthorBooks);
            queryable = queryable.Include(x => x.BookCategories).ThenInclude(x => x.CategoryBooks);
            queryable = queryable.Include(x => x.BookLanguages).ThenInclude(x => x.LanguageBooks);
            queryable = queryable.Include(x => x.BookInterpreters).ThenInclude(x => x.InterpreterBooks);
            queryable = queryable.Include(p => p.Publisher);
            queryable = queryable.Include(p => p.Location);


            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters().Where(field => field.DeletedDate == null); //Girdiğim filter koşulunu sağlayan verileri topla.
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.ToPaginateAsync(index, size, from: 0, cancellationToken);
        }

        public async Task<IPaginate<Book>> GetListWithAuthors(Expression<Func<Book, bool>> predicate, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Books.AsQueryable();

            //queryable.Include(x => x.BookAuthors).ThenInclude(x => x.AuthorBooks);
            queryable = queryable.Include(x => x.BookAuthors).ThenInclude(x => x.AuthorBooks);
            queryable = queryable.Include(x => x.BookCategories).ThenInclude(x => x.CategoryBooks);
            queryable = queryable.Include(x => x.BookLanguages).ThenInclude(x => x.LanguageBooks);
            queryable = queryable.Include(x => x.BookInterpreters).ThenInclude(x => x.InterpreterBooks);
            queryable = queryable.Include(p => p.Publisher);
            queryable = queryable.Include(p => p.Location);


            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters().Where(field => field.DeletedDate == null); //Girdiğim filter koşulunu sağlayan verileri topla.
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.ToPaginateAsync(index, size, from: 0, cancellationToken);
        }

    }
}
