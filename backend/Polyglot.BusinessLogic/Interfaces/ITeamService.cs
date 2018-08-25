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

        #endregion Teams

        #region Translators

        Task<IEnumerable<TranslatorDTO>> GetAllTranslatorsAsync();

        Task<TranslatorDTO> GetTranslatorAysnc(int id);

        Task<IEnumerable<RightDTO>> GetTranslatorRightsAsync(int translatorId);

        Task<double> GetTranslatorRatingValueAsync(int translatorId);

        Task<TranslatorDTO> SetTranslatorRight(int userId, int teamId, RightDefinition definition);

        Task<TranslatorDTO> RemoveTranslatorRight(int userId, int teamId, RightDefinition definition);

        #endregion Translators
    }
}
