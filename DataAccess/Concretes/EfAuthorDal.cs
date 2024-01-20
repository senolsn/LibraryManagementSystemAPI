﻿using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concretes
{
    public class EfAuthorDal : EfRepositoryBase<Author, LibraryAPIDbContext>, IAuthorDal
    {
        public EfAuthorDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}