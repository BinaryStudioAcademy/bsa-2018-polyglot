using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.NoSQL_Models;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IComplexStringService
    {
        Task<ComplexStringDTO> PutAsync(ComplexStringDTO entity);
        Task<bool> TryDeleteAsync(int id);
        Task<ComplexStringDTO> PostAsync(ComplexStringDTO entity);
    }
}
