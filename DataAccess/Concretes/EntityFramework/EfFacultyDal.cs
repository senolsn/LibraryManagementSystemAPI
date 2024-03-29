﻿using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfFacultyDal : EfRepositoryBase<Faculty, LibraryAPIDbContext>, IFacultyDal
    {
        public EfFacultyDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
