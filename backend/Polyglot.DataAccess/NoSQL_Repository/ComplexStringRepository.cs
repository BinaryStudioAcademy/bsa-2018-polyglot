using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Polyglot.DataAccess.NoSQL_Models;
namespace Polyglot.DataAccess.NoSQL_Repository
{
    public class ComplexStringRepository : Repository<ComplexString>, IComplexStringRepository
    {
        private const string CollectionName = "ComplexStrings";
        private readonly MongoDataContext _dataContext;

        public ComplexStringRepository(MongoDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<ComplexString> Collection => _dataContext.MongoDatabase.GetCollection<ComplexString>(CollectionName);

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
