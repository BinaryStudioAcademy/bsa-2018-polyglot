using Polyglot.DataAccess.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.ViewsRepository
{
    public interface IViewData<QueryEntity> where QueryEntity : QueryType
    {
        Task<List<QueryEntity>> GetAllAsync();

        Task<QueryEntity> GetAsync(Expression<Func<QueryEntity, bool>> predicate);

        Task<List<QueryEntity>> GetAllAsync(Expression<Func<QueryEntity, bool>> predicate);
    }
}
