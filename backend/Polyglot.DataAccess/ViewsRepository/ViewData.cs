
using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.ViewsRepository
{
    class ViewData<QueryEntity> : IViewData<QueryEntity> where QueryEntity : QueryType
    {
        protected DbContext context;

        private List<Expression<Func<QueryEntity, object>>> includeExpressions;

        public ViewData(DbContext c)
        {
            this.context = c;
            includeExpressions = new List<Expression<Func<QueryEntity, object>>>();
        }

        public async Task<List<QueryEntity>> GetAllAsync()
        {
            return await context.Query<QueryEntity>().ToListAsync();
        }

        public async Task<QueryEntity> GetAsync(Expression<Func<QueryEntity, bool>> predicate)
        {
            return await context.Query<QueryEntity>().FirstOrDefaultAsync(predicate);
        }
    }
}
