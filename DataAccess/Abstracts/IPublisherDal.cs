﻿using Core.DataAccess.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstracts
{
    public interface IPublisherDal : IAsyncRepository<Publisher>
    {
    }
}
