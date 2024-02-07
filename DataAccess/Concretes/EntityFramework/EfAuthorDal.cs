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

            var queryable = base.Context.Authors.AsQueryable();
            queryable = queryable.Include(x => x.AuthorBooks).ThenInclude(x => x.BookAuthors); 

            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
