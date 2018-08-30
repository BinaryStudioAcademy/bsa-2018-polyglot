using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.QueryTypes;

namespace Polyglot.DataAccess.SqlRepository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
		private DbContext context;
        private Dictionary<Type, object> repositories;

		public UnitOfWork(DbContext c)
		{
			context = c;
            repositories = new Dictionary<Type, object>();
            /*context.Database.ExecuteSqlCommand(@"CREATE VIEW View_UserRights AS
                                                    SELECT TranslatorId AS UserId, Definition AS RightDefinition, ProjectId FROM TeamTranslators
                                                    INNER JOIN TranslatorRight ON TeamTranslatorId = TeamTranslators.Id
                                                    INNER JOIN Rights On RightId = Rights.id
                                                    INNER JOIN ProjectTeams on TeamTranslators.TeamId = ProjectTeams.TeamId");*/

        }

        public IRepository<T> GetRepository<T>() 
            where T : DbEntity, new() 
        {
            var targetType = typeof(T);
            if (repositories.ContainsKey(targetType))
            {
                return repositories[targetType] as IRepository<T>;
            }
            else
            {
                var repoInstance = new Repository<T>(context);
                repositories.Add(targetType, repoInstance);
                return repoInstance;
            }
        }

        public async Task<List<UserRights>> GetUserRights()
        {
            return await context.Query<UserRights>().ToListAsync();
        }

        public async Task<int> SaveAsync()
		{
			return await context.SaveChangesAsync();
		}

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
