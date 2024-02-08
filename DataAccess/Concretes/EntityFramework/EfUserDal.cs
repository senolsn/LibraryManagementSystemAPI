using Core.DataAccess.Repositories;
using Core.Entities.Concrete;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfUserDal : EfRepositoryBase<User, LibraryAPIDbContext>, IUserDal
    {
        private readonly LibraryAPIDbContext _context;
        public EfUserDal(LibraryAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async override Task<ICollection<User>> GetListAsync(Expression<Func<User, bool>> predicate, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Users.AsQueryable();

            queryable = queryable.Include(u => u.UserDepartments).ThenInclude(x => x.DepartmentUsers);
            queryable = queryable.Include(u => u.UserDepositBooks);
            queryable = queryable.Include(f => f.Faculty);

            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters().Where(field => field.DeletedDate == null); //Girdiğim filter koşulunu sağlayan verileri topla.
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.ToListAsync(cancellationToken);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            var result = from operationClaim in Context.OperationClaims
                         join userOperationClaim in Context.UserOperationClaims
                             on operationClaim.OperationClaimId equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.UserId
                         select new OperationClaim { OperationClaimId = operationClaim.OperationClaimId, OperationName = operationClaim.OperationName };

            return result.ToList();
        }
    }

}
