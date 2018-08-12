using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.SqlRepository;

namespace Polyglot.BusinessLogic.Implementations
{
    public class UserService : CRUDService, IUserService
    {
        public UserService(IUnitOfWork uow, IMapper mapper)
            :base(uow, mapper)
        {

        }
    }
}
