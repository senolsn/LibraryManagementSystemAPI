using Core.DataAccess.Repositories;
using Core.Entities.Abstract;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfCategoryDal : EfRepositoryBase<Category, LibraryAPIDbContext>, ICategoryDal
    {
        public EfCategoryDal(LibraryAPIDbContext context) : base(context)
        {
        }

        public override async Task<Category?> GetAsync(Expression<Func<Category, bool>> predicate, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = base.Context.Categories.AsQueryable();

            queryable = queryable.Include(x => x.CategoryBooks).ThenInclude(x => x.BookCategories); 


            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
