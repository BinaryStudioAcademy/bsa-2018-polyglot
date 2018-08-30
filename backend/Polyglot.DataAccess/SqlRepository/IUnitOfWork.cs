using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.QueryTypes;

namespace Polyglot.DataAccess.SqlRepository
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>()
            where T : DbEntity, new();


        Task<int> SaveAsync();

        Task<List<UserRights>> GetUserRights();

    }
}
