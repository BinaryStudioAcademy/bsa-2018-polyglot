using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<R, T>()
            where R : IRepository<T>
            where T : Entity, new();


        Task<int> SaveAsync();
	}
}
