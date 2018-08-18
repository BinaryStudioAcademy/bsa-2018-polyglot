using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ITeamService : ICRUDService<Team, TeamDTO>
    {
        #region Teams

        Task<IEnumerable<TeamDTO>> GetAllTeamsPrevsAsync();

        Task<IEnumerable<TeamDTO>> GetAllTeamsAsync();

        Task<IEnumerable<TranslatorDTO>> GetTeamTranslatorsAsync(int teamId);
        
        Task<TeamDTO> FormTeamAsync(int[] translatorIds, int managerId);

        Task<bool> TryDisbandTeamAsync(int teamId);

        #endregion Teams

        #region Translators

        Task<IEnumerable<TranslatorDTO>> GetAllTranslatorsAsync();

        Task<TranslatorDTO> GetTranslatorAysnc(int id);

        Task<IEnumerable<RightDTO>> GetTranslatorRightsAsync(int translatorId);

        Task<double> GetTranslatorRatingValueAsync(int translatorId);

        #endregion Translators
    }
}
