using Polyglot.DataAccess.NoSQL_Models;
namespace Polyglot.DataAccess.NoSQL_Repository
{
    public class ComplexStringRepository : Repository<ComplexString>, IComplexStringRepository
    {
        public ComplexStringRepository(Microsoft.Extensions.Options.IOptions<Settings> settings) : base(settings)
        {
        }
    }
}
