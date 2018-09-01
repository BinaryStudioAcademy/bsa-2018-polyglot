using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces.SignalR
{
    public interface ISignalRWorkspaceService
    {
        Task ChangedTranslation(string groupName, int translationId);
        
        Task CommentAdded(string groupName, int commentId);
        
        Task CommentDeleted(string groupName, int commentId);

        Task CommentEdited(string groupName, int commentId);

        Task ComplexStringAdded(string groupName, int complexStringId);

        Task ComplexStringRemoved(string groupName, int complexStringId);

        Task LanguageRemoved(string groupName, int languageId);

        Task LanguagesAdded(string groupName, int[] languagesIds);

        Task LanguageTranslationCommitted(string groupName, int languageId);
    }
}
