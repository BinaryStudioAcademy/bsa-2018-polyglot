
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Helpers;
using Polyglot.DataAccess.QueryTypes;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services
{
    public class RightService : CRUDService<Right, RightDTO>, IRightService
    {
        IUserService userService;
        IProjectService projectService;
        public RightService(IUnitOfWork uow, IMapper mapper, IUserService userService, IProjectService projectService)
            : base(uow, mapper)
        {
            this.userService = userService;
            this.projectService = projectService;
        }

        public async Task<TranslatorDTO> SetTranslatorRight(int userId, int teamId, RightDefinition definition)
        {
            var translator = (await uow.GetRepository<Team>().GetAsync(teamId))
                .TeamTranslators.FirstOrDefault(t => t.UserProfile.Id == userId);

            var right = (await uow.GetRepository<Right>().GetAllAsync())
                    .FirstOrDefault(r => r.Definition == definition);
            translator.TranslatorRights.Add(new TranslatorRight()
            {
                RightId = right.Id,
                TeamTranslatorId = translator.Id
            });

            var newTranslator = uow.GetRepository<TeamTranslator>().Update(translator);
            await uow.SaveAsync();

            return newTranslator != null ? mapper.Map<TranslatorDTO>(newTranslator) : null;
        }

        public async Task<TranslatorDTO> RemoveTranslatorRight(int userId, int teamId, RightDefinition definition)
        {
            var translator = (await uow.GetRepository<Team>().GetAsync(teamId)).
                TeamTranslators.FirstOrDefault(t => t.UserProfile.Id == userId);

            var right = (await uow.GetRepository<Right>().GetAllAsync())
                    .FirstOrDefault(r => r.Definition == definition);
            var translatorRight = translator.TranslatorRights
                .FirstOrDefault(tr => tr.RightId == right.Id && tr.TeamTranslatorId == translator.Id);

            translator.TranslatorRights.Remove(translatorRight);

            var newTranslator = uow.GetRepository<TeamTranslator>().Update(translator);
            await uow.SaveAsync();

            return newTranslator != null ? mapper.Map<TranslatorDTO>(newTranslator) : null;
        }

        public async Task<bool> CheckIfCurrentUserCanInProject(RightDefinition definition, int projectId)
        {
            int userId = (await CurrentUser.GetCurrentUserProfile()).Id;
            var rightInProject = (await uow.GetViewData<UserRights>()
                .GetAsync(ur => ur.ProjectId == projectId && ur.UserId == userId && ur.RightDefinition == definition));
            if(rightInProject == null)
            {
                return false;
            }
            return true;
        }
    }
}
