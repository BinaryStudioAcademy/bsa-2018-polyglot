using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>()
            where T : Entity, new();


        Task<int> SaveAsync();
	}
}
