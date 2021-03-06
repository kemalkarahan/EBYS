﻿using Entities.Concrete;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOfficeDal : IRepository<Office>
    {
        Task<int> RetrieveId(Expression<Func<Office, bool>> filter);
    }
}
