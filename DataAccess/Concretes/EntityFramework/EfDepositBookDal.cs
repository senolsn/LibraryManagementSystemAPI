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
    public class EfDepositBookDal : EfRepositoryBase<DepositBook, LibraryAPIDbContext>, IDepositBookDal
    {
        public EfDepositBookDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
