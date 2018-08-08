using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.NoSQL_Models;
namespace Polyglot.DataAccess.NoSQL_Repository
{
    public class ComplexStringRepository : Repository<ComplexString>, IComplexStringRepository
    {
        private const string CollectionName = "ComplexStrings";
        private readonly IMongoDataContext _dataContext;

        public ComplexStringRepository(IMongoDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<ComplexString> Collection => _dataContext.ComplexStrings;

        public async Task<IEnumerable<ComplexString>> GetAllByProjectId(int projectId)
        {
            try
            {
                return await Collection
                                .Find(x => x.ProjectId == projectId).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
