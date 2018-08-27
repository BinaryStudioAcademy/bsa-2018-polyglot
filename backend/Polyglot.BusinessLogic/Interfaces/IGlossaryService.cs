using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IGlossaryService : ICRUDService<Glossary, GlossaryDTO>
    {
        Task<GlossaryDTO> AddString(int glossaryId, GlossaryStringDTO glossaryString);
        Task<GlossaryDTO> UpdateString(int glossaryId, GlossaryStringDTO glossaryString);
        Task<bool> DeleteString(int glossaryId, int glossaryStringId);
    }
}
