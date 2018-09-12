
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
        private readonly ICurrentUser _currentUser;

        public RightService(IUnitOfWork uow, IMapper mapper, IUserService userService, IProjectService projectService, ICurrentUser currentUser)
            : base(uow, mapper)
        {
            this.userService = userService;
            this.projectService = projectService;
            _currentUser = currentUser;
        }

        public async Task<TranslatorDTO> SetTranslatorRight(int userId, int teamId, RightDefinition definition)
        {
            var translator = (await uow.GetRepository<TeamTranslator>().GetAsync(t => t.TeamId == teamId && t.TranslatorId == userId));

            var right = (await uow.GetRepository<Right>().GetAsync(r => r.Definition == definition));
            if(right == null)
            {
                right = (await uow.GetRepository<Right>().CreateAsync(new Right()
                {
                    Definition = definition
                }));
            }
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
            var translator = (await uow.GetRepository<TeamTranslator>().GetAsync(t => t.TeamId == teamId && t.TranslatorId == userId));


            var right = (await uow.GetRepository<Right>().GetAsync(r => r.Definition == definition));

            var translatorRight = translator.TranslatorRights
                .FirstOrDefault(tr => tr.RightId == right.Id && tr.TeamTranslatorId == translator.Id);

            translator.TranslatorRights.Remove(translatorRight);

            var newTranslator = uow.GetRepository<TeamTranslator>().Update(translator);
            await uow.SaveAsync();

            return newTranslator != null ? mapper.Map<TranslatorDTO>(newTranslator) : null;
        }

        public async Task<bool> CheckIfCurrentUserCanInProject(RightDefinition definition, int projectId)
        {
            var userRights = (await _currentUser.GetRightsInProject(projectId)).Select(r => r.RightDefinition).Distinct();
            if(userRights.Contains(definition))
            {
                return true;
            }
            return false;
        }

        public async Task<List<RightDefinition>> GetUserRightsInProject(int projectId)
        {
            var userRights = await _currentUser.GetRightsInProject(projectId);
            return userRights.Select(r => r.RightDefinition).Distinct().ToList();
        }

        public async Task<List<UserRights>> GetUserRights()
        {
            var userRights = await _currentUser.GetRights();
            return userRights.ToList();
        }
    }
}
