using Core.DataAccess.Repositories;
using Core.Entities.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;
using System.Threading;
using System;
using System.Threading.Tasks;
using Core.DataAccess.Paging;

namespace DataAccess.Abstracts
{
    public interface IBookDal:IAsyncRepository<Book>
    {
        Task<IPaginate<Book>> GetListWithAuthors(
           Expression<Func<Book, bool>> predicate,
           int index = 0,
           int size = 10,
           bool withDeleted = false,
           bool enableTracking = true,
           CancellationToken cancellationToken = default);
    }
}
