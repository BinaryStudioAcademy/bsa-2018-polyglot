using Polyglot.Common.DTOs.NoSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polyglot.Hubs
{
    public interface ISignalrWorkspaceService
    {
        Task ChangedTranslation(string groupName, TranslationDTO entity);

        Task CommentAdded(string groupName, IEnumerable<CommentDTO> comments);
    }
}
