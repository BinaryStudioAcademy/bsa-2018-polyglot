using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.NoSQL_Models;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IComplexStringService: ICRUDService<ComplexString,int> 
    {
        Task<IEnumerable<ComplexString>> GetListByProjectId(int projectId);
    }
}
