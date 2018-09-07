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
using Polyglot.DataAccess.Entities.Chat;
using Polyglot.DataAccess.MongoRepository;
using System;

namespace Polyglot.BusinessLogic.Services
{
    public class TeamsService : CRUDService<Team, TeamDTO>, ITeamService
    {
        INotificationService notificationService;
		private readonly IMongoRepository<DataAccess.MongoModels.ComplexString> stringsProvider;

		public TeamsService(IUnitOfWork uow, IMapper mapper, INotificationService notificationService, IMongoRepository<DataAccess.MongoModels.ComplexString> rep)
            :base(uow, mapper)

        {
			this.stringsProvider = rep;
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
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null || currentUser.UserRole != Role.Manager)
                return null;

            var userRepo = uow.GetRepository<UserProfile>();

            List<TeamTranslator> translators = new List<TeamTranslator>();
            UserProfile currentTranslator;
            List<DialogParticipant> teamChatDialogParticipants = new List<DialogParticipant>()
            {
                new DialogParticipant() { Participant = currentUser }
            };

            foreach (var id in receivedTeam.TranslatorIds)
            {
                currentTranslator = await userRepo.GetAsync(id);
                if (currentTranslator != null && currentTranslator.UserRole == Role.Translator)
                {
                    translators.Add(new TeamTranslator
                    {
                        UserProfile = currentTranslator
                    });

                    if (currentTranslator.Id != currentUser.Id)
                        teamChatDialogParticipants.Add(new DialogParticipant() { Participant = currentTranslator });
                }
            }

            if (translators.Count < 1)
                return null;

            Team newTeam = await uow.GetRepository<Team>().CreateAsync(
                    new Team()
                    {
                        TeamTranslators = translators,
                        CreatedBy = currentUser,
                        Name = receivedTeam.Name
                    });
            
                newTeam.TeamTranslators = translators;
                newTeam.CreatedBy = currentUser;
                newTeam.Name = receivedTeam.Name;

            newTeam = await uow.GetRepository<Team>().CreateAsync(newTeam);


                await uow.SaveAsync();
                foreach(var translator in newTeam.TeamTranslators)
                {

                    await notificationService.SendNotification(new NotificationDTO
                    {
                        SenderId = currentUser.Id,
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

            var teamChatDialog = new ChatDialog()
            {
                DialogName = newTeam.Name,
                DialogParticipants = teamChatDialogParticipants,
                DialogType = ChatGroup.chatTeam,
                Identifier = newTeam.Id
            };
            await uow.GetRepository<ChatDialog>().CreateAsync(teamChatDialog);
            await uow.SaveAsync();

            return newTeam != null ? mapper.Map<TeamDTO>(newTeam) : null;
        }

        public async Task<bool> TryDisbandTeamAsync(int teamId)
        {
            await uow.GetRepository<Team>().DeleteAsync(teamId);
            var success = await uow.SaveAsync() > 0;

            if (success)
            {
                var targetTeamDialog = uow.GetRepository<ChatDialog>()
                .GetAsync(d => d.DialogType == ChatGroup.chatTeam && d.Identifier == teamId)
                ?.Id;
                if (targetTeamDialog.HasValue)
                {
                    await uow.GetRepository<ChatDialog>().DeleteAsync(targetTeamDialog.Value);
                }
                await uow.SaveAsync();
            }
            return success;
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

			foreach(var p in teamsProjects)
			{				
				var targetProject = await uow.GetRepository<Project>().GetAsync(p.ProjectId);

				var strings = await stringsProvider.GetAllAsync(str => str.ProjectId == targetProject.Id);
				int languagesAmount = targetProject.ProjectLanguageses.Count;
				int max = strings.Count * languagesAmount;
				int currentProgress = 0;
				foreach (var str in strings)
				{
					currentProgress += str.Translations.Count;
				}
				p.Progress = Convert.ToInt32((Convert.ToDouble(currentProgress) / Convert.ToDouble(max)) * 100);
			}

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

        public async Task<TranslatorDTO> ActivateUserInTeam(int userId, int teamId)
        {
            var teamTranslator = await uow.GetRepository<TeamTranslator>().GetAsync(t => t.TranslatorId == userId && t.TeamId == teamId);
            teamTranslator.IsActivated = true;
            teamTranslator =  await uow.GetRepository<TeamTranslator>().Update(teamTranslator);
            await uow.SaveAsync();
            return mapper.Map<TranslatorDTO>(teamTranslator);
        }


        #endregion Translators
    }
}
