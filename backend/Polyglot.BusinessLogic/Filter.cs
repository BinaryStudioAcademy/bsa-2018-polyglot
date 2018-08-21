using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.MongoRepository;
using Polyglot.DataAccess.SqlRepository;

namespace Polyglot.BusinessLogic
{
    public class Filter
    {
        public static async Task<IEnumerable<T>> FiltrationSqlModelAsync<T>(Expression<Func<T, bool>> predicate,IUnitOfWork uow) where T : Entity, new()
        {
            var result = await uow.GetRepository<T>().GetAllAsync(predicate);
            return result;
        }

        public static async Task<IEnumerable<T>> FiltrationMongoModelAsync<T>(Expression<Func<T, bool>> predicate, IMongoRepository<T> repository) where T : Entity, new()
        {
            var result = await repository.GetAllAsync(predicate);
            return result;
        }

        public class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }
    }
}
