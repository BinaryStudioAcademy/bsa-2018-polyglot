using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<UserProfileDTO> GetByUidAsync(string uid)
        {
            var repository = uow.GetRepository<UserProfile>();
            var user = await repository.GetAllAsync(u => u.Uid == uid);

            if (user.Count > 1)
            {
                return null;
            }

            return mapper.Map<UserProfile, UserProfileDTO>(user.First());
        }

        public async Task<bool> IsExistByUidAsync(string uid)
        {
            var repository = uow.GetRepository<UserProfile>();

            var result = await repository.GetAllAsync(u => u.Uid == uid);

            return result.Count > 0;
        }
    }
}
