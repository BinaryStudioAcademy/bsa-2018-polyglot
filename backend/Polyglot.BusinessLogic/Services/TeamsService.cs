using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Helpers;
using Polyglot.DataAccess.SqlRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polyglot.Core.Authentication;

namespace Polyglot.BusinessLogic.Services
{
    public class TeamsService : CRUDService<Team, TeamDTO>, ITeamService
    {
        INotificationService notificationService;
        public TeamsService(IUnitOfWork uow, IMapper mapper, INotificationService notificationService)
            :base(uow, mapper)
        {
            this.notificationService = notificationService;
        }

        public async Task<IEnumerable<TeamPrevDTO>> GetAllTeamsAsync()
        {
            var user = await CurrentUser.GetCurrentUserProfile();
            IEnumerable<Team> result = new List<Team>();

            if (user.UserRole == Role.Translator)
            {
                var translatorTeams = await uow.GetRepository<TeamTranslator>().GetAllAsync(x => x.TranslatorId == user.Id);
                var allTeams = await uow.GetRepository<Team>().GetAllAsync();
                result = allTeams.Where(x => translatorTeams.Any(y => y.TeamId == x.Id));
            }
            else
            {
                 result = await uow.GetRepository<Team>().GetAllAsync(x => x.CreatedBy == user);
            }

            return mapper.Map<IEnumerable<TeamPrevDTO>>(result);
        }   
        
        public async Task<TeamDTO> FormTeamAsync(ReceiveTeamDTO receivedTeam)
        {
            var userRepo = uow.GetRepository<UserProfile>();

            List<TeamTranslator> translators = new List<TeamTranslator>();
            UserProfile currentTranslator;
            foreach (var id in receivedTeam.TranslatorIds)
            {
                currentTranslator = await userRepo.GetAsync(id);
                if (currentTranslator != null && currentTranslator.UserRole == Role.Translator)
                {
                    translators.Add(new TeamTranslator
                    {
                        UserProfile = currentTranslator
                    });
                }
            }

            if (translators.Count < 1)
                return null;

            var manager = await CurrentUser.GetCurrentUserProfile();

            if (manager.UserRole == Role.Manager)
            {

                Team newTeam = await uow.GetRepository<Team>().CreateAsync(new Team());
                await uow.SaveAsync();

                newTeam.TeamTranslators = translators;
                newTeam.CreatedBy = manager;
                newTeam.Name = receivedTeam.Name;
                newTeam = await uow.GetRepository<Team>().Update(newTeam);
                await uow.SaveAsync();
                foreach(var translator in newTeam.TeamTranslators)
                {

                    await notificationService.SendNotification(new NotificationDTO
                    {
                        SenderId = manager.Id,
                        Message = $"You received an invitation in team {newTeam.Name}",
                        ReceiverId = translator.TranslatorId,
                        NotificationAction = NotificationAction.JoinTeam,
                        Payload = newTeam.Id,  
                        Options = new List<OptionDTO>()
                        {
                            new OptionDTO()
                            {
                                OptionDefinition = OptionDefinition.Accept
                            },
                            new OptionDTO()
                            {
                                OptionDefinition = OptionDefinition.Decline
                            }
                        }
                    });
                }

                return newTeam != null ? mapper.Map<TeamDTO>(newTeam) : null;
            }

            return null;
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

            if (team?.TeamTranslators?.Any() != true)
                return null;

            
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

            var teamsProjects = mapper.Map<IEnumerable<TeamProjectDTO>>(team.ProjectTeams);

            return new TeamDTO()
            {
                Id = team.Id,
				Name = team.Name,
                TeamTranslators = teamTranslators.ToList(),
                TeamProjects = teamsProjects.ToList()
            };
           
        }

        public override async Task<TeamDTO> PutAsync(TeamDTO entity)
        {
            if (uow != null)
            {
                var teamRepository = uow.GetRepository<Team>();
                var target = await teamRepository.Update(mapper.Map<Team>(entity));
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
                
                var tLanguages = await uow.GetMidRepository<TranslatorLanguage>()
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
                var translatorLanguages = await uow.GetMidRepository<TranslatorLanguage>()
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

        public async Task<TeamTranslator> ActivateUserInTeam(int userId, int teamId)
        {
            var teamTranslator = await uow.GetRepository<TeamTranslator>().GetAsync(t => t.TranslatorId == userId && t.TeamId == teamId);
            teamTranslator.IsActivated = true;
            teamTranslator =  uow.GetRepository<TeamTranslator>().Update(teamTranslator);
            await uow.SaveAsync();
            return teamTranslator;
        }


        #endregion Translators
    }
}
