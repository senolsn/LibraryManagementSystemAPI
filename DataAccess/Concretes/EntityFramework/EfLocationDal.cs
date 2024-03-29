﻿using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfLocationDal : EfRepositoryBase<Location, LibraryAPIDbContext>, ILocationDal
    {
        public EfLocationDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
