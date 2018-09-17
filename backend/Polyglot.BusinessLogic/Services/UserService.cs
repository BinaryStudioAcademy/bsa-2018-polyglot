using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.SqlRepository;
using Polyglot.DataAccess.Entities;
using Polyglot.Common.DTOs;
using Polyglot.Core.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Polyglot.BusinessLogic.Services
{
    [Authorize]
    public class UserService : CRUDService<UserProfile, UserProfileDTO>, IUserService
    {
        private readonly ICurrentUser _currentUser;

        public UserService(IUnitOfWork uow, IMapper mapper, ICurrentUser currentUser)
            :base(uow, mapper)
        {
            _currentUser = currentUser;
        }

        public async Task<UserProfileDTO> GetByUidAsync()
        {
            var user = await _currentUser.GetCurrentUserProfile();
            if (user == null)
            {
                return null;
            }

            return mapper.Map<UserProfile, UserProfileDTO>(user);
        }

        public async Task<bool> IsExistByUidAsync()
        {
            var user = await _currentUser.GetCurrentUserProfile();
            return user != null;
        }

        public async Task<bool> PutUserBool(UserProfileDTO userProfileDTO)
        {
            var result = await uow.GetRepository<UserProfile>().UpdateBool((mapper.Map<UserProfile>(userProfileDTO)));
            if (result)
            {
                await uow.SaveAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<UserProfileDTO>> GetUsersByNameStartsWith(string startsWith)
        {
            int currentUserId = (await _currentUser.GetCurrentUserProfile()).Id;
            return mapper.Map<IEnumerable<UserProfileDTO>>(await uow.GetRepository<UserProfile>()
                .GetAllAsync(u => u.FullName.ToLower().StartsWith(startsWith.ToLower()) && u.Id != currentUserId));
        }
    }
}
