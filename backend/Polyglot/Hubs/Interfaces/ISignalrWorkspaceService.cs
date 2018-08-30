﻿using Polyglot.Common.DTOs.NoSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polyglot.Hubs
{
    public interface ISignalrWorkspaceService
    {
        Task ChangedTranslation(string groupName, TranslationDTO entity);

        Task CommentAdded(string groupName, IEnumerable<CommentDTO> comment);
        
        Task CommentDeleted(string groupName, IEnumerable<CommentDTO> comment);

        Task CommentEdited(string groupName, IEnumerable<CommentDTO> comment);

        Task ComplexStringAdded(string groupName, int complexStringId);

        Task ComplexStringRemoved(string groupName, int complexStringId);

        Task LanguageRemoved(string groupName, int languageId);

        Task LanguagesAdded(string groupName, int[] languagesIds);

        Task LanguageTranslationCommitted(string groupName, int languageId);
    }
}
