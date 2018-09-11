using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ITeamService : ICRUDService<Team, TeamDTO>
    {
        #region Teams
        
        Task<IEnumerable<TeamPrevDTO>> GetAllTeamsAsync();
        
        Task<TeamDTO> FormTeamAsync(ReceiveTeamDTO receivedTeam);

        Task<bool> TryDisbandTeamAsync(int teamId);

        Task<TeamDTO> TryAddTeamAsync(TeamTranslatorsDTO teamTranslators);

        #endregion Teams

        #region Translators

        Task<IEnumerable<TranslatorDTO>> GetAllTranslatorsAsync();
		Task<IEnumerable<TranslatorDTO>> GetFilteredtranslators(int prof, int[] languages);
		Task<TranslatorDTO> GetTranslatorAysnc(int id);

        Task<double> GetTranslatorRatingValueAsync(int translatorId);

        Task<TranslatorDTO> ActivateUserInTeam(int userId, int teamId);

        #endregion Translators
    }
}
