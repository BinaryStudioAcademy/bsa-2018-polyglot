using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.SqlRepository;

using Polyglot.DataAccess.Entities;
using Polyglot.Common.DTOs;

namespace Polyglot.BusinessLogic.Services
{
    public class UserService : CRUDService<UserProfile, UserProfileDTO>, IUserService
    {
        public UserService(IUnitOfWork uow, IMapper mapper)
            :base(uow, mapper)
        {

        }
    }
}
