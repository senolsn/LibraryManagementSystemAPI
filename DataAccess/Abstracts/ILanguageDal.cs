﻿using Core.DataAccess.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstracts
{
    public interface ILanguageDal : IAsyncRepository<Language>
    {
    }
}
