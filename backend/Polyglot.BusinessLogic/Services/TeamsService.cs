﻿using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Helpers;
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

        public async Task<IEnumerable<TeamPrevDTO>> GetAllTeamsAsync()
        {
            var teams = await uow.GetRepository<Team>().GetAllAsync();

            if (teams != null && teams.Count > 0)
                return mapper.Map<IEnumerable<TeamPrevDTO>>(teams);
            else
                return new List<TeamPrevDTO>();
        }   
        
        public async Task<TeamDTO> FormTeamAsync(int[] translatorIds, int managerId)
        {
            var userRepo = uow.GetRepository<UserProfile>();
       //     UserProfile manager = await userRepo.GetAsync(managerId);
       //     if (manager == null || manager.UserRole != UserProfile.Role.Manager)
       //         return null;
       
            List<TeamTranslator> translators = new List<TeamTranslator>();
            UserProfile currentTranslator;
            foreach (var id in translatorIds)
            {
                currentTranslator = await userRepo.GetAsync(id);
                if (currentTranslator != null && currentTranslator.UserRole == Role.Translator)
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

        #region Overrides


        public override async Task<TeamDTO> GetOneAsync(int teamId)
        {
            var team = await uow.GetRepository<Team>().GetAsync(teamId);
            if (team != null && team.TeamTranslators != null && team.TeamTranslators.Count > 0)
            {
                var translators = team.TeamTranslators;
                // вычисляем рейтинги переводчиков
                Dictionary<int, double> ratings = new Dictionary<int, double>();
                IEnumerable<double> ratingRatesSequence;
                double? currentRating;

                foreach (var t in translators)
                {
                    ratingRatesSequence = t.UserProfile?.Ratings.Select(r => r.Rate);
                    if (ratingRatesSequence.Count() < 1)
                        currentRating = 0.0d;
                    else
                        currentRating = ratingRatesSequence.Average();

                    ratings.Add(t.UserProfile.Id, currentRating.HasValue ? currentRating.Value : 0.0d);
                }
                //маппим и после маппинга заполняем рейтинг
                var teamTranslators = mapper.Map<IEnumerable<TeamTranslator>, IEnumerable<TranslatorDTO>>(translators, opt => opt.AfterMap((src, dest) =>
                {
                    var translatorsList = dest.ToList();
                    for (int i = 0; i < translatorsList.Count; i++)
                    {
                        translatorsList[i].Rating = ratings[translatorsList[i].UserId];
                    }

                }));

                return new TeamDTO()
                {
                    Id = team.Id,
                    TeamTranslators = teamTranslators.ToList()
                };
            }
            else
                return null;
        }

        public override async Task<TeamDTO> PutAsync(TeamDTO entity)
        {
            if (uow != null)
            {
                var teamRepository = uow.GetRepository<Team>();
                var target = teamRepository.Update(mapper.Map<Team>(entity));
                if (target != null)
                {
                    await uow.SaveAsync();
                    return mapper.Map<TeamDTO>(target);
                }
            }
            return null;
        }

        #endregion Overrides

        #region Translators


        public async Task<IEnumerable<TranslatorDTO>> GetAllTranslatorsAsync()
        {
            var translators = await uow.GetRepository<UserProfile>()
                .GetAllAsync(u => u.UserRole == Role.Translator);

            if (translators != null && translators.Count > 0)
            {
                
                var tLanguages = await uow.GetRepository<TranslatorLanguage>()
                    .GetAllAsync();
                // вычисляем рейтинги переводчиков
                Dictionary<int, double> ratings = new Dictionary<int, double>();
                IEnumerable<double> ratingRatesSequence;
                double? currentRating;

                foreach (var t in translators)
                {
                    ratingRatesSequence = t.Ratings.Select(r => r.Rate);
                    if (ratingRatesSequence.Count() < 1)
                        currentRating = 0.0d;
                    else
                        currentRating = ratingRatesSequence.Average();

                    ratings.Add(t.Id, currentRating.HasValue ? currentRating.Value : 0.0d);
                }
                //маппим и после маппинга заполняем рейтинг
                return mapper.Map<IEnumerable<UserProfile>, IEnumerable<TranslatorDTO>>(translators, opt => opt.AfterMap((src, dest) =>
                {
                    var translatorsList = dest.ToList();
                    IEnumerable<TranslatorLanguage> translatorLanguages;
                    for (int i = 0; i < translatorsList.Count; i++)
                    {
                        translatorsList[i].Rating = ratings[translatorsList[i].UserId];
                        // добавляем инфо о языках
                        translatorLanguages = tLanguages.Where(tl => tl.TranslatorId == translatorsList[i].UserId);
                        if (translatorLanguages != null && translatorLanguages.Count() > 0)
                            translatorsList[i].TranslatorLanguages = mapper.Map<IEnumerable<TranslatorLanguageDTO>>(translatorLanguages);
                    }

                }));
            }
            else
                return new List<TranslatorDTO>();
        }

        public async Task<TranslatorDTO> GetTranslatorAysnc(int id)
        {
            var translator = await uow.GetRepository<UserProfile>().GetAsync(id);
            if (translator != null && translator.UserRole == Role.Translator)
            {
                var translatorLanguages = await uow.GetRepository<TranslatorLanguage>()
                    .GetAllAsync(tl => tl.TranslatorId == translator.Id);

                return mapper.Map<UserProfile, TranslatorDTO>(translator, opt => opt.AfterMap((src, dest) =>
                {
                    var ratingRatesSequence = src.Ratings.Select(r => r.Rate);
                    if (ratingRatesSequence.Count() < 1)
                        dest.Rating = 0.0d;
                    else
                        dest.Rating = src.Ratings.Select(r => r.Rate).Average();

                    // добавляем инфо о языках
                    if (translatorLanguages != null && translatorLanguages.Count() > 0)
                        dest.TranslatorLanguages = mapper.Map<IEnumerable<TranslatorLanguageDTO>>(translatorLanguages);
                }));
            }
            else
                return null;
        }

        public async Task<double> GetTranslatorRatingValueAsync(int translatorId)
        {
            var translator = await uow.GetRepository<UserProfile>().GetAsync(translatorId);
            if (translator != null && translator.UserRole == Role.Translator)
            {
                var ratingRatesSequence = translator.Ratings.Select(r => r.Rate);
                if (ratingRatesSequence.Count() > 0)
                    return ratingRatesSequence.Average();
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
