using Core.DataAccess.Repositories;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IDepositBookDal : IAsyncRepository<DepositBook>
    {
        Task<ICollection<GetAllDepositBooksResponse>> GetAllAsync(Expression<Func<DepositBook, bool>> predicate, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default);
    }
}
