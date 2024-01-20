using Core.DataAccess.Repositories;
using Core.Entities.Concrete;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfUserDal : EfRepositoryBase<User, LibraryAPIDbContext>, IUserDal
    {
        public EfUserDal(LibraryAPIDbContext context) : base(context)
        {
        }

        public List<OperationClaim> GetClaims(User user)
        {
            var result = from operationClaim in Context.OperationClaims
                         join userOperationClaim in Context.UserOperationClaims
                             on operationClaim.OperationId equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.UserId
                         select new OperationClaim { OperationId = operationClaim.OperationId, OperationName = operationClaim.OperationName };

            return result.ToList();
        }
    }

}
