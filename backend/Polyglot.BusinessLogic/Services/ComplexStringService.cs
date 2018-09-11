using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nest;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.BusinessLogic.Interfaces.SignalR;
using Polyglot.BusinessLogic.TranslationServices;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.Common.Helpers.SignalR;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Elasticsearch;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.MongoModels;
using Polyglot.DataAccess.MongoRepository;
using Polyglot.DataAccess.SqlRepository;
using ComplexString = Polyglot.DataAccess.MongoModels.ComplexString;
using TranslationDTO = Polyglot.Common.DTOs.NoSQL.TranslationDTO;

namespace Polyglot.BusinessLogic.Services
{
    public class ComplexStringService : IComplexStringService
    {
        private readonly IMongoRepository<ComplexString> _complexStringRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageProvider _fileStorageProvider;
        private readonly ICRUDService<UserProfile, UserProfileDTO> _userProfileService;
        private readonly ISignalRWorkspaceService _signalRWorkspaceService;
        private readonly IElasticClient _elasticClient;
        private readonly TranslationTimerService _timerService;


        public ComplexStringService(IMongoRepository<ComplexString> complexStringRepository, IMapper mapper, IUnitOfWork unitOfWork, IFileStorageProvider fileStorageProvider,
                                    ICRUDService<UserProfile, UserProfileDTO> userService, ISignalRWorkspaceService signalRWorkspaceWorkspace, IElasticClient elasticClient, TranslationTimerService timerService)
        {
            this._unitOfWork = unitOfWork;
            this._complexStringRepository = complexStringRepository;
            this._mapper = mapper;
            this._fileStorageProvider = fileStorageProvider;
            this._userProfileService = userService;
            this._signalRWorkspaceService = signalRWorkspaceWorkspace;
            this._elasticClient = elasticClient;
            this._timerService = timerService;
        }

        public async Task<IEnumerable<ComplexStringDTO>> GetListAsync()
        {
            var targets = await _complexStringRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ComplexStringDTO>>(targets);
        }

        public async Task<ComplexStringDTO> GetComplexString(int identifier)
        {

            var target = await _complexStringRepository.GetAsync(identifier);
            if (target != null)
            {
                ComplexStringDTO stringDTO = _mapper.Map<ComplexStringDTO>(target);
                foreach (var translation in stringDTO.Translations)
                {
                    if(translation.AssignedTranslatorId != 0)
                    {
                        UserProfileDTO user = await _userProfileService.GetOneAsync(translation.AssignedTranslatorId);
                        translation.AssignedTranslatorAvatarUrl = user.AvatarUrl;
                        translation.AssignedTranslatorName = user.FullName;
                    }
                }
                return stringDTO;
            }

            return null;

        }

        public async Task<IEnumerable<TranslationDTO>> GetStringTranslationsAsync(int identifier)
        {
            var target = await _complexStringRepository.GetAsync(identifier);
            if (target != null)
            {
                return _mapper.Map<IEnumerable<TranslationDTO>>(target.Translations);
            }

            return null;
        }

      
        public async Task<TranslationDTO> SetStringTranslation(int identifier, TranslationDTO translation)
        {
            var target = await _complexStringRepository.GetAsync(identifier);
            if (target != null)
            {
                var currentTranslation = _mapper.Map<Translation>(translation);
                currentTranslation.Id = Guid.NewGuid();
                currentTranslation.History.Add(new AdditionalTranslation
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    TranslationValue = translation.TranslationValue,
                    UserId = translation.UserId
                });
                //currentTranslation.AssignedTranslatorId = translation.AssignedTranslatorId;

                var targetTranslationDublicateIndex = target.Translations.FindIndex(t => t.LanguageId == translation.LanguageId);
                if (targetTranslationDublicateIndex >= 0)
                {
                    target.Translations[targetTranslationDublicateIndex] = currentTranslation;
                }
                else
                {
                    target.Translations.Add(currentTranslation);
                }
                var result = await _complexStringRepository.Update(target);


                var targetProjectId = target.ProjectId;
                await _signalRWorkspaceService.LanguageTranslationCommitted($"{Group.project}{targetProjectId}", translation.LanguageId);
                await _signalRWorkspaceService.ChangedTranslation($"{Group.complexString}{identifier}", identifier);

                return (_mapper.Map<ComplexStringDTO>(result)).Translations.LastOrDefault();
                //var translationNew = (_mapper.Map<ComplexStringDTO>(result)).Translations.LastOrDefault();
                //translationNew.AssignedTranslatorAvatarUrl = translation.AssignedTranslatorAvatarUrl;
                //translationNew.AssignedTranslatorName = translation.AssignedTranslatorName;
                //return translationNew;
            }
            return null;

        }

        public async Task<TranslationDTO> EditStringTranslation(int identifier, TranslationDTO translation)
        {
            var target = await _complexStringRepository.GetAsync(identifier);
            if (target != null)
            {
                var translationsList = _mapper.Map<List<Translation>>(target.Translations);
                var currentTranslation = translationsList.FirstOrDefault(x => x.Id == translation.Id);
                currentTranslation.History.Add(new AdditionalTranslation
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    TranslationValue = translation.TranslationValue,
                    UserId = translation.UserId
                });
                currentTranslation.TranslationValue = translation.TranslationValue;
                currentTranslation.UserId = translation.UserId;
                currentTranslation.CreatedOn = DateTime.Now;
                currentTranslation.AssignedTranslatorId = translation.AssignedTranslatorId;

                target.Translations = translationsList;

                var result = await _complexStringRepository.Update(target);

                var targetProjectId = target.ProjectId;
                await _signalRWorkspaceService.LanguageTranslationCommitted($"{Group.project}{targetProjectId}", translation.LanguageId);
                await _signalRWorkspaceService.ChangedTranslation($"{Group.complexString}{identifier}", identifier);
                //var translationNew = (_mapper.Map<ComplexStringDTO>(result)).Translations.FirstOrDefault(x => x.Id == translation.Id);
                //translationNew.AssignedTranslatorAvatarUrl = translation.AssignedTranslatorAvatarUrl;
                //translationNew.AssignedTranslatorName = translation.AssignedTranslatorName;
                //return translationNew;
                return (_mapper.Map<ComplexStringDTO>(result)).Translations.FirstOrDefault(x => x.Id == translation.Id);
            }
            return null;

        }

        public async Task<TranslationDTO> ConfirmTranslation(int identifier, TranslationDTO translation)
        {
            var target = await _complexStringRepository.GetAsync(identifier);
            if (target != null)
            {
                var translationsList = _mapper.Map<List<Translation>>(target.Translations);
                var currentTranslation = translationsList.FirstOrDefault(x => x.Id == translation.Id);
                currentTranslation.IsConfirmed = true;

                target.Translations = translationsList;

                var result = await _complexStringRepository.Update(_mapper.Map<ComplexString>(target));

                var targetProjectId = target.ProjectId;

                await _signalRWorkspaceService.ChangedTranslation($"{Group.complexString}{identifier}", identifier);
                return (_mapper.Map<ComplexStringDTO>(result)).Translations.FirstOrDefault(x => x.Id == translation.Id);
            }
            return null;

        }

        public async Task<TranslationDTO> UnConfirmTranslation(int identifier, TranslationDTO translation)
        {
            var target = await _complexStringRepository.GetAsync(identifier);
            if (target != null)
            {
                var translationsList = _mapper.Map<List<Translation>>(target.Translations);
                var currentTranslation = translationsList.FirstOrDefault(x => x.Id == translation.Id);
                currentTranslation.IsConfirmed = false;

                target.Translations = translationsList;

                var result = await _complexStringRepository.Update(_mapper.Map<ComplexString>(target));

                var targetProjectId = target.ProjectId;

                await _signalRWorkspaceService.ChangedTranslation($"{Group.complexString}{identifier}", identifier);
                return (_mapper.Map<ComplexStringDTO>(result)).Translations.FirstOrDefault(x => x.Id == translation.Id);
            }
            return null;
        }

        public async Task<TranslationDTO> RevertTranslationHistory(int identifier, Guid translationId, Guid historyId)
        {
            var complexString = await _complexStringRepository.GetAsync(identifier);
            var currentTranslation = complexString.Translations.FirstOrDefault(x => x.Id == translationId);
            var currentHistory = currentTranslation?.History.FirstOrDefault(x => x.Id == historyId);

            if (currentTranslation!=null&&currentHistory!=null)
            {           
                currentTranslation?.History.RemoveAll(x=>x.CreatedOn>currentHistory?.CreatedOn);

                currentTranslation.TranslationValue = currentHistory.TranslationValue;
                currentTranslation.CreatedOn = currentHistory.CreatedOn;
                currentTranslation.UserId = currentHistory.UserId;

                var result = await _complexStringRepository.Update(complexString);

                var targetProjectId = complexString.ProjectId;
                await _signalRWorkspaceService.LanguageTranslationCommitted($"{Group.project}{targetProjectId}", (int)currentTranslation.LanguageId);
                await _signalRWorkspaceService.ChangedTranslation($"{Group.complexString}{identifier}", identifier);

                return _mapper.Map<ComplexStringDTO>(result).Translations.FirstOrDefault(x => x.Id == translationId);
            }
            return null;
        }


		public async Task<AdditionalTranslationDTO> AddOptionalTranslation(int stringId, Guid translationId, string value)
		{
			var targetString = await _complexStringRepository.GetAsync(stringId);
			var targetTranslation = targetString.Translations.FirstOrDefault(t => t.Id == translationId);

			targetTranslation.OptionalTranslations.Add(
				new AdditionalTranslation() {
					TranslationValue = value,
					UserId = (await CurrentUser.GetCurrentUserProfile()).Id,
					CreatedOn = DateTime.Now
					//Type = Translation.TranslationType.Human
				});

			var result = await _complexStringRepository.Update(targetString);
			return _mapper.Map<AdditionalTranslationDTO>
				(result.Translations.FirstOrDefault(t => t.Id == translationId).OptionalTranslations.LastOrDefault());
		}

		public async Task<IEnumerable<OptionalTranslationDTO>> GetOptionalTranslations(int stringId, Guid translationId)
		{
			List<OptionalTranslationDTO> target = new List<OptionalTranslationDTO>();

			var source = (await _complexStringRepository.GetAsync(stringId)).Translations.FirstOrDefault(t => t.Id == translationId).OptionalTranslations;

			foreach(var opt in source)
			{
				var user = await _unitOfWork.GetRepository<UserProfile>().GetAsync(opt.UserId);

				target.Add(
					new OptionalTranslationDTO() {
						UserPictureURL = user.AvatarUrl,
						UserName = user.FullName,
						DateTime = opt.CreatedOn,
						TranslationValue = opt.TranslationValue
					});
			}
			target.Reverse();
			return target;
		}

		public async Task<ComplexStringDTO> ModifyComplexString(ComplexStringDTO entity)
        {
            var target = await _complexStringRepository.Update(_mapper.Map<ComplexString>(entity));
            if (target != null)
            {
                return _mapper.Map<ComplexStringDTO>(target);
            }
            return null;

        }

        public async Task<bool> DeleteComplexString(int identifier)
        {
            ComplexString toDelete = await _complexStringRepository.GetAsync(identifier);
            int projectId = toDelete.ProjectId;

            if (toDelete.PictureLink != null)
                await _fileStorageProvider.DeleteFileAsync(toDelete.PictureLink);

            await _unitOfWork.GetRepository<Polyglot.DataAccess.Entities.ComplexString>().DeleteAsync(identifier);
            await _unitOfWork.SaveAsync();
            await _complexStringRepository.DeleteAsync(identifier);

            await this._signalRWorkspaceService.ComplexStringRemoved($"{Group.project}{projectId}", identifier);

            return true;
        }

        public async Task<ComplexStringDTO> AddComplexString(ComplexStringDTO entity)
        {
            var sqlComplexString = new Polyglot.DataAccess.Entities.ComplexString
            {
                TranslationKey = entity.Key,
                ProjectId = entity.ProjectId
            };
            var savedEntity = await _unitOfWork.GetRepository<Polyglot.DataAccess.Entities.ComplexString>().CreateAsync(sqlComplexString);
            await _unitOfWork.SaveAsync();
            entity.Id = savedEntity.Id;
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = (await CurrentUser.GetCurrentUserProfile()).Id;

            var mappedItem = _mapper.Map<ComplexString>(entity);
            mappedItem.Tags = entity.Tags.Select(x => x.Id).ToList();

            var target = await _complexStringRepository.CreateAsync(mappedItem);

            if (target != null)
            {
                await _signalRWorkspaceService.ComplexStringAdded($"{Group.project}{target.ProjectId}", target.Id);
                return _mapper.Map<ComplexStringDTO>(target);
            }
            return null;
        }

        public async Task<IEnumerable<CommentDTO>> SetComment(int identifier, CommentDTO comment , int itemsOnPage)
        {
            var target = await _complexStringRepository.GetAsync(identifier);
            if (target != null)
            {
                var currentComment = _mapper.Map<Comment>(comment);
                currentComment.Id = Guid.NewGuid();
                target.Comments.Add(currentComment);
                var result = await _complexStringRepository.Update(target);
                var commentsWithUsers = await GetFullUserInComments(_mapper.Map<IEnumerable<CommentDTO>>(result.Comments));
                
                await _signalRWorkspaceService.СommentsChanged($"{Group.complexString}{identifier}", identifier);
                
                return commentsWithUsers.OrderByDescending(x => x.CreatedOn).Take(itemsOnPage);

            }
            return null;

        }

        public async Task<IEnumerable<CommentDTO>> DeleteComment(int identifier, Guid commentId)
        {
            var target = await _complexStringRepository.GetAsync(identifier);
            if (target != null)
            {
                
                var currentComment = target.Comments.FirstOrDefault(x => x.Id == commentId);

                var comments = target.Comments;
                comments.Remove(currentComment);
                target.Comments = comments;

                var result = await _complexStringRepository.Update(target);
                
                var commentsWithUsers = await GetFullUserInComments(_mapper.Map<IEnumerable<CommentDTO>>(result.Comments));
                await _signalRWorkspaceService.СommentsChanged($"{Group.complexString}{identifier}", identifier);
                return commentsWithUsers.Reverse();

            }
            return null;
        }

        public async Task<IEnumerable<CommentDTO>> EditComment(int identifier, CommentDTO comment)
        {
            var target = await _complexStringRepository.GetAsync(identifier);
            if (target != null)
            {
                var comments = _mapper.Map<List<Comment>>(target.Comments);
                var currentComment = comments.FirstOrDefault(x => x.Id == comment.Id);
                
                currentComment.Text = comment.Text;
                
                target.Comments = comments;

                var result = await _complexStringRepository.Update(target);
                var commentsWithUsers = await GetFullUserInComments(_mapper.Map<IEnumerable<CommentDTO>>(result.Comments));

                await _signalRWorkspaceService.СommentsChanged($"{Group.complexString}{identifier}", identifier);

                return commentsWithUsers.Reverse();

            }
            return null;

        }

                
        public async Task<IEnumerable<CommentDTO>> GetCommentsAsync(int identifier)
        {
            var target = await _complexStringRepository.GetAsync(identifier);
            if (target != null)
            {
                return await GetFullUserInComments(_mapper.Map<IEnumerable<CommentDTO>>(target.Comments));
            }

            return null;
        }
        
        public async Task<IEnumerable<CommentDTO>> GetCommentsWithPaginationAsync(int identifier, int itemsOnPage, int page)
        {
            var skipItems = itemsOnPage * page;

            var target = await _complexStringRepository.GetAsync(identifier);

            if (target != null)
            {
                
                var paginatedComments = target.Comments.OrderByDescending(x => x.CreatedOn).Skip(skipItems).Take(itemsOnPage);
                return await GetFullUserInComments(_mapper.Map<IEnumerable<CommentDTO>>(paginatedComments));
            }

            return null;

        }
        
        private async Task<IEnumerable<CommentDTO>> GetFullUserInComments(IEnumerable<CommentDTO> comments)
        {
            foreach(var com in comments)
            {
                com.User = await _userProfileService.GetOneAsync(com.User.Id);
            }
            return comments;
        }

        public async Task<IEnumerable<HistoryDTO>> GetHistoryAsync(int identifier, Guid translationId,int itemsOnPage, int page)
        {
            var complexString = await GetComplexString(identifier);
            var translation = complexString.Translations.FirstOrDefault(t => t.Id == translationId);

            if (translation == null || complexString == null)
                return null;

            var history = new List<HistoryDTO>();

            translation.History = translation.History.OrderBy(x => x.CreatedOn).ToList();

            if (translation.History.Count == 0)
            {
                var user = await _userProfileService.GetOneAsync(translation.UserId);
                history.Add(new HistoryDTO {
                    Id = translation.Id,
                    UserName = user.FullName,
                    AvatarUrl = user.AvatarUrl,
                    Action = "translated",
                    From = $"{complexString.OriginalValue}",
                    To = $"{translation.TranslationValue}",
                    When = translation.CreatedOn
                });
            }
            else
            {
                var first = await _userProfileService.GetOneAsync(translation.History[0].UserId);
                history.Add(new HistoryDTO
                {
                    Id = translation.History[0].Id,
                    UserName = first.FullName,
                    AvatarUrl = first.AvatarUrl,
                    Action = "translated",
                    From = $"{complexString.OriginalValue}",
                    To = $"{translation.History[0].TranslationValue}",
                    When = translation.History[0].CreatedOn
                });

                for (int i = 1; i < translation.History.Count; i++)
                {
                    var user = await _userProfileService.GetOneAsync(translation.History[i].UserId);
                    history.Add(new HistoryDTO
                    {
                        Id = translation.History[i].Id,
                        UserName = user.FullName,
                        AvatarUrl = user.AvatarUrl,
                        Action = "changed",
                        From = $"{translation.History[i-1].TranslationValue}",
                        To = $"{translation.History[i].TranslationValue}",
                        When = translation.History[i].CreatedOn
                    });
                }
            }

            history.Reverse();
            var skipItems = itemsOnPage * page;
            var paginatedHistory = history.Skip(skipItems).Take(itemsOnPage);

            return paginatedHistory;
        }
        
        public async Task<string> ReIndex()
        {
            var allPosts = (await _complexStringRepository.GetAllAsync()).ToList();

            var res = await ElasticRepository.ReIndex(allPosts);

            return res;
        }
        
        public async Task ChangeStringStatus(int id, bool status, string groupName)
        {
            if (status)
            {
                await _signalRWorkspaceService.ComplexStringTranslatingStarted(groupName, id);
            }
            else
            {
                await _signalRWorkspaceService.ComplexStringTranslatingFinished(groupName, id);
            }
        }
    }
}
