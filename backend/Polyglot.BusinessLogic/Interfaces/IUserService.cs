using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        #region UserProfiles

        Task<UserProfile> GetUserProfileAsync(int id);

        Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync(int id);

        Task<UserProfile> AddUserProfileAsync(UserProfile profile);

        Task<UserProfile> UpdateMangerAsync(UserProfile profile);

        Task<bool> TryDeleteUserProfileAsync(int id);

        #endregion

        #region Managers

        Task<Manager> GetManagerAsync(int id);

        Task<IEnumerable<Manager>> GetAllManagersAsync(int id);

        Task<Manager> AddManagerAsync(Manager manager);

        Task<Manager> UpdateManagerAsync(Manager manager);

        Task<bool> TryDeleteManagerAsync(int id);

        #endregion

        #region Translators

        Task<Translator> GetTranslatorAsync(int id);

        Task<IEnumerable<Translator>> GetAllTranslatorsAsync(int id);

        Task<Translator> AddTranslatorAsync(Translator translator);

        Task<Translator> UpdateTranslatorAsync(Translator translator);

        Task<bool> TryDeleteTranslatorAsync(int id);

        #endregion
    }
}
