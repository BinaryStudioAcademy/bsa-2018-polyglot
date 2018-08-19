using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;

namespace Polyglot.BusinessLogic
{
    public class Filter
    {
        public static async Task<IEnumerable<T>> FiltrationAsync<T>(Expression<Func<T, bool>> predicate,IUnitOfWork uow) where T : Entity, new()
        {
            var result = await uow.GetRepository<T>().GetAllAsync(predicate);
            return result;
        }
    }
}
