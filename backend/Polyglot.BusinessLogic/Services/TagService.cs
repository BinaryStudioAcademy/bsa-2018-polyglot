using System;
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
        public TagService(IUnitOfWork uow, IMapper mapper) : base(uow,mapper) { }

        public async Task<IEnumerable<TagDTO>> AddTagsToProject(IEnumerable<TagDTO> tags,int projectId)
        {
            List<TagDTO> result = new List<TagDTO>();
            var tagRepository = uow.GetRepository<Tag>();
            int countOfNewTags = 0;
            var projectRepo =  uow.GetRepository<Project>();
            var project = await projectRepo.GetAsync(projectId);

            foreach (var tag in tags)
            {
                if (tag.Id == 0)
                {
                    project.Tags.Add(mapper.Map<Tag>(tag));
                    countOfNewTags++;
                }
                else
                {
                    result.Add(tag);
                }
            }

            var target = await uow.GetRepository<Project>().Update(project);
            if (target != null)
            {
                await uow.SaveAsync();
            }

            result.AddRange(mapper.Map<List<TagDTO>>(target.Tags.Skip(Math.Max(0, target.Tags.Count - countOfNewTags))));

            return result;
        }
    }
}
