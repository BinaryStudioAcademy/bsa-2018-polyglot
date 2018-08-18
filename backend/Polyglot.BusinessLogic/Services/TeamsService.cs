using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services
{
    public class TeamsService : CRUDService<Team, TeamDTO>, ITeamService
    {
        public TeamsService(IUnitOfWork uow, IMapper mapper)
            :base(uow, mapper)
        {

        }

        public async Task<IEnumerable<TeamDTO>> GetAllTeamsAsync()
        {
            var teams = await uow.GetRepository<Team>().GetAllAsync();
            if (teams != null)
                return mapper.Map<IEnumerable<TeamDTO>>(teams);
            else
                return null;
        }   

        public async Task<IEnumerable<TeamDTO>> GetAllTeamsPrevsAsync()
        {
            var teams = await uow.GetRepository<Team>().GetAllAsync();
            if (teams != null)
                return mapper.Map<IEnumerable<TeamDTO>>(teams);
            else
                return null;
        }

        public async Task<IEnumerable<TranslatorDTO>> GetTeamTranslatorsAsync(int teamId)
        {
            var team = await uow.GetRepository<Team>().GetAsync(teamId);
#warning тут не вычисляется рейтинг переводчика
            if (team != null)
                return mapper.Map<IEnumerable<TranslatorDTO>>(team?.TeamTranslators);
            return null;
        }

        public async Task<TeamDTO> FormTeamAsync(int[] translatorIds, int managerId)
        {
            var userRepo = uow.GetRepository<UserProfile>();
       //     UserProfile manager = await userRepo.GetAsync(managerId);
       //     if (manager == null || manager.UserRole != UserProfile.Role.Manager)
       //         return null;

#warning протестировать создание команды
            List<TeamTranslator> translators = new List<TeamTranslator>();
            UserProfile currentTranslator;
            foreach (var id in translatorIds)
            {
                currentTranslator = await userRepo.GetAsync(id);
                if (currentTranslator != null && currentTranslator.UserRole == UserProfile.Role.Translator)
                {
                    translators.Add(new TeamTranslator()
                    {
                        UserProfile = currentTranslator
                    });
                }
            }

            if (translators.Count < 1)
                return null;

#warning неизвестно что делать с менеджером
            Team newTeam = await uow.GetRepository<Team>().CreateAsync(new Team());
            await uow.SaveAsync();

            newTeam.TeamTranslators = translators;
            newTeam = uow.GetRepository<Team>().Update(newTeam);
            await uow.SaveAsync();

            return newTeam != null ? mapper.Map<TeamDTO>(newTeam) : null;
        }

        public async Task<bool> TryDisbandTeamAsync(int teamId)
        {
#warning протестировать не удаляются ли пользователи

            await uow.GetRepository<Team>().DeleteAsync(teamId);
            return await uow.SaveAsync() > 0;
        }

        #region Translators


        public async Task<IEnumerable<TranslatorDTO>> GetAllTranslatorsAsync()
        {
            var translators = await uow.GetRepository<UserProfile>()
                .GetAllAsync(u => u.UserRole == UserProfile.Role.Translator);

            if (translators != null)
            {
                // вычисляем рейтинг
                Dictionary<int, double> ratings = new Dictionary<int, double>();
                foreach (var t in translators)
                {
                    ratings.Add(t.Id, t.Ratings.Select(r => r.Rate).Average());
                }
                //маппим и после маппинга заполняем рейтинг
                return mapper.Map<IEnumerable<UserProfile>, IEnumerable<TranslatorDTO>>(translators, opt => opt.AfterMap((src, dest) =>
                {
                    var translatorsList = dest.ToList();
                    for (int i = 0; i < translatorsList.Count; i++)
                    {
                        translatorsList[i].Rating = ratings[translatorsList[i].Id];
                    }

                }));
            }
            else
                return null;
        }

        public async Task<TranslatorDTO> GetTranslatorAysnc(int id)
        {
            var translator = await uow.GetRepository<UserProfile>().GetAsync(id);
            if (translator != null && translator.UserRole == UserProfile.Role.Translator)
            {
                return mapper.Map<UserProfile, TranslatorDTO>(translator, opt => opt.AfterMap((src, dest) =>
                {
                    dest.Rating = src.Ratings.Select(r => r.Rate).Average();
                }));
            }
            else
                return null;
        }

        public async Task<double> GetTranslatorRatingValueAsync(int translatorId)
        {
#warning протестить правильно ли получаем rating
            var translator = await uow.GetRepository<UserProfile>().GetAsync(translatorId);
            if (translator != null && translator.UserRole == UserProfile.Role.Translator)
            {
                return translator.Ratings.Select(r => r.Rate).Average();
            }

            return 0.0d;
        }

        public Task<IEnumerable<RightDTO>> GetTranslatorRightsAsync(int translatorId)
        {
// TODO IMPLEMENT
            throw new System.NotImplementedException();
        }

        #endregion Translators
    }
}
