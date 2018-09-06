using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ITagService : ICRUDService<Tag,TagDTO>
    {
        Task<IEnumerable<TagDTO>> AddTagsToProject(IEnumerable<TagDTO> tags,int projectId);
    }
}
