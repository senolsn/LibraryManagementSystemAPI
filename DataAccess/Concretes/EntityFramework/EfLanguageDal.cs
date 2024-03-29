﻿using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfLanguageDal : EfRepositoryBase<Language, LibraryAPIDbContext>, ILanguageDal
    {
        public EfLanguageDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
