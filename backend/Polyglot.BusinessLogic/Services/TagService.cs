
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;

namespace Polyglot.BusinessLogic.Services
{
    class TagService : CRUDService<Tag, TagDTO>, ITagService
    {
        public TagService(IUnitOfWork uow, IMapper mapper) : base(uow,mapper)
        {
            
        }

        public Task<IEnumerable<TagDTO>> GetProjectTags(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<TagDTO>> AddTagsToProject(IEnumerable<TagDTO> tags)
        {
            List<TagDTO> result = new List<TagDTO>();
            var repo = uow.GetRepository<Tag>();
            foreach (var tag in tags)
            {
                if (tag.Id == 0)
                {
                    await repo.CreateAsync(mapper.Map<Tag>(tag));
                    await uow.SaveAsync();
                    var newTag = await repo.GetLastAsync();
                    result.Add(mapper.Map<TagDTO>(newTag));
                }
                else
                {
                    result.Add(tag);
                }
            }
            return result;
        }
    }
}
