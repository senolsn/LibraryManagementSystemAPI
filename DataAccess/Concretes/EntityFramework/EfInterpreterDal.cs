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
    public class EfInterpreterDal : EfRepositoryBase<Interpreter, LibraryAPIDbContext>, IInterpreterDal
    {
        public EfInterpreterDal(LibraryAPIDbContext context) : base(context)
        {
        }

        public async override Task<Interpreter?> GetAsync(Expression<Func<Interpreter, bool>> predicate, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = base.Context.Interpreters.AsQueryable();

            queryable = queryable.Include(x => x.InterpreterBooks).ThenInclude(x => x.BookInterpreters);


            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
