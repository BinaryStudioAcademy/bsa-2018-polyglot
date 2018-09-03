using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.QueryTypes;
using Polyglot.DataAccess.ViewsRepository;

namespace Polyglot.DataAccess.SqlRepository
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : Entity, new();

        IViewData<T> GetViewData<T>()
            where T : QueryType, new();


        Task<int> SaveAsync();
    }
}
