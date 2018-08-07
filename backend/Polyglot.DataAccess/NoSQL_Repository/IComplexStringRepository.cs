using Polyglot.DataAccess.NoSQL_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.NoSQL_Repository
{
    public interface IComplexStringRepository : IRepository<ComplexString>
    {
        Task<IEnumerable<ComplexString>> GetAllByProjectId(int projectId);
    }
}
