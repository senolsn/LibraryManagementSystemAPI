using Core.DataAccess.Repositories;
using Core.Entities.Concrete;
using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstracts
{
    public interface IUserDal : IAsyncRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
