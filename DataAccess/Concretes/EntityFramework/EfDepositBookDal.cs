using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfDepositBookDal : EfRepositoryBase<DepositBook, LibraryAPIDbContext>, IDepositBookDal
    {
        private readonly LibraryAPIDbContext _context;
        public EfDepositBookDal(LibraryAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async override Task<ICollection<DepositBook>> GetListAsync(Expression<Func<DepositBook, bool>> predicate, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = _context.DepositBooks.AsQueryable();

            queryable = queryable.Include(u => u.User);
            queryable = queryable.Include(u => u.Book);

            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters().Where(field => field.DeletedDate == null); //Girdiğim filter koşulunu sağlayan verileri topla.
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.ToListAsync(cancellationToken);
        }



        public async Task<ICollection<GetAllDepositBooksResponse>> GetAllAsync(Expression<Func<DepositBook, bool>> predicate, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {

            var result = from db in _context.DepositBooks
                         join b in _context.Books on db.BookId equals b.BookId
                         join u in _context.Users on db.UserId equals u.UserId
                         select new GetAllDepositBooksResponse
                         {
                             DepositBookId = db.DepositBookId,
                             User = u,
                             Book = new Book
                             {
                                 BookId = b.BookId,
                                 BookName = b.BookName,
                                 BookAuthors = b.BookAuthors.Select(a => new Author
                                 {
                                     AuthorId = a.AuthorId,
                                     AuthorFirstName = a.AuthorFirstName,
                                     AuthorLastName = a.AuthorLastName
                                 }).ToList()
                             },
                             DepositDate = db.DepositDate,
                             Status = db.Status,
                             EscrowDate = db.EscrowDate,
                             DateShouldBeEscrow = db.DateShouldBeEscrow
                         };
            var queryable = result.AsQueryable();

            //queryable = queryable.Include(x => x.Book);
            //queryable = queryable.Include(x => x.User);



            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.ToListAsync(cancellationToken);
        }
    }
}
