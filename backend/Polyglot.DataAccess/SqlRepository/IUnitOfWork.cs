﻿using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;

namespace Polyglot.DataAccess.SqlRepository
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>()
            where T : DbEntity, new();


        Task<int> SaveAsync();
	}
}
