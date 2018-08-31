using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.Common.Helpers.SignalR;
using Polyglot.Core.Authentication;
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
        private readonly IMongoRepository<ComplexString> repository;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IFileStorageProvider provider;
        private readonly ICRUDService<UserProfile, UserProfileDTO> userSevice;
        private readonly ISignalRWorkspaceService signalRService;


        public ComplexStringService(IMongoRepository<ComplexString> repository, IMapper mapper, IUnitOfWork uow, IFileStorageProvider provider,
                                    ICRUDService<UserProfile, UserProfileDTO> userService, ISignalRWorkspaceService signalRWorkspace)
        {
            this.uow = uow;
            this.repository = repository;
            this.mapper = mapper;
            this.provider = provider;
            this.userSevice = userService;
            this.signalRService = signalRWorkspace;
        }

        public async Task<IEnumerable<ComplexStringDTO>> GetListAsync()
        {

            var targets = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<ComplexStringDTO>>(targets);
        }

        public async Task<ComplexStringDTO> GetComplexString(int identifier)
        {

            var target = await repository.GetAsync(identifier);
            if (target != null)
            {
                return mapper.Map<ComplexStringDTO>(target);
            }

            return null;

        }

        public async Task<IEnumerable<TranslationDTO>> GetStringTranslationsAsync(int identifier)
        {
            var target = await repository.GetAsync(identifier);
            if (target != null)
            {
                return mapper.Map<IEnumerable<TranslationDTO>>(target.Translations);
            }

            return null;
        }

        public async Task<TranslationDTO> SetStringTranslation(int identifier, TranslationDTO translation)
        {
            var target = await repository.GetAsync(identifier);
            if (target != null)
            {
                var currentTranslation = mapper.Map<Translation>(translation);
                currentTranslation.Id = Guid.NewGuid();
                var targetTranslationDublicateIndex = target.Translations.FindIndex(t => t.LanguageId == translation.LanguageId);
                if (targetTranslationDublicateIndex >= 0)
                {
                    target.Translations[targetTranslationDublicateIndex] = currentTranslation;
                }
                else
                {
                    target.Translations.Add(currentTranslation);
                }
                var result = await repository.Update(mapper.Map<ComplexString>(target));


                var targetProjectId = target.ProjectId;
                await signalRService.LanguageTranslationCommitted($"{Group.project}{targetProjectId}", translation.LanguageId);
                await signalRService.ChangedTranslation($"{Group.complexString}{identifier}", identifier);

                return (mapper.Map<ComplexStringDTO>(result)).Translations.LastOrDefault();
            }
            return null;

        }

        public async Task<TranslationDTO> EditStringTranslation(int identifier, TranslationDTO translation)
        {
            var target = await repository.GetAsync(identifier);
            if (target != null)
            {
                var translationsList = mapper.Map<List<Translation>>(target.Translations);
                var currentTranslation = translationsList.FirstOrDefault(x => x.Id == translation.Id);
                currentTranslation.History.Add(new AdditionalTranslation
                {
                    CreatedOn = currentTranslation.CreatedOn,
                    TranslationValue = currentTranslation.TranslationValue,
                    UserId = currentTranslation.UserId
                });
                currentTranslation.TranslationValue = translation.TranslationValue;
                currentTranslation.UserId = translation.UserId;
                currentTranslation.CreatedOn = DateTime.Now;
                target.Translations = translationsList;

                var result = await repository.Update(mapper.Map<ComplexString>(target));

                var targetProjectId = target.ProjectId;
                await signalRService.LanguageTranslationCommitted($"{Group.project}{targetProjectId}", translation.LanguageId);
                await signalRService.ChangedTranslation($"{Group.complexString}{identifier}", identifier);

                return (mapper.Map<ComplexStringDTO>(result)).Translations.FirstOrDefault(x => x.Id == translation.Id);
            }
            return null;

        }

		public async Task<AdditionalTranslationDTO> AddOptionalTranslation(int stringId, Guid translationId, string value)
		{
			var targetString = await repository.GetAsync(stringId);
			var targetTranslation = targetString.Translations.FirstOrDefault(t => t.Id == translationId);

			targetTranslation.OptionalTranslations.Add(
				new AdditionalTranslation() {
					TranslationValue = value,
					UserId = (await CurrentUser.GetCurrentUserProfile()).Id,
					CreatedOn = DateTime.Now
					//Type = Translation.TranslationType.Human
				});

			var result = await repository.Update(targetString);
			return mapper.Map<AdditionalTranslationDTO>
				(result.Translations.FirstOrDefault(t => t.Id == translationId).OptionalTranslations.LastOrDefault());
		}

		public async Task<IEnumerable<OptionalTranslationDTO>> GetOptionalTranslations(int stringId, Guid translationId)
		{
			List<OptionalTranslationDTO> target = new List<OptionalTranslationDTO>();

			var source = (await repository.GetAsync(stringId)).Translations.FirstOrDefault(t => t.Id == translationId).OptionalTranslations;

			foreach(var opt in source)
			{
				var user = await uow.GetRepository<UserProfile>().GetAsync(opt.UserId);

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
            var target = await repository.Update(mapper.Map<ComplexString>(entity));
            if (target != null)
            {
                return mapper.Map<ComplexStringDTO>(target);
            }
            return null;

        }

        public async Task<bool> DeleteComplexString(int identifier)
        {
            ComplexString toDelete = await repository.GetAsync(identifier);
            int projectId = toDelete.ProjectId;

            if (toDelete.PictureLink != null)
                await provider.DeleteFileAsync(toDelete.PictureLink);

            await uow.GetRepository<Polyglot.DataAccess.Entities.ComplexString>().DeleteAsync(identifier);
            await uow.SaveAsync();
            await repository.DeleteAsync(identifier);

            await this.signalRService.ComplexStringRemoved($"{Group.project}{projectId}", identifier);

            return true;
        }

        public async Task<ComplexStringDTO> AddComplexString(ComplexStringDTO entity)
        {
            var sqlComplexString = new Polyglot.DataAccess.Entities.ComplexString
            {
                TranslationKey = entity.Key,
                ProjectId = entity.ProjectId
            };
            var savedEntity = await uow.GetRepository<Polyglot.DataAccess.Entities.ComplexString>().CreateAsync(sqlComplexString);
            await uow.SaveAsync();
            entity.Id = savedEntity.Id;
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = (await CurrentUser.GetCurrentUserProfile()).Id;
            var target = await repository
                .CreateAsync(mapper.Map<ComplexString>(entity));
            if (target != null)
            {
                await signalRService.ComplexStringAdded($"{Group.project}{target.ProjectId}", target.Id);
                return mapper.Map<ComplexStringDTO>(target);
            }
            return null;
        }

        public async Task<IEnumerable<CommentDTO>> SetComment(int identifier, CommentDTO comment)
        {
            var target = await repository.GetAsync(identifier);
            if (target != null)
            {
                var currentComment = mapper.Map<Comment>(comment);
                currentComment.Id = Guid.NewGuid();
                target.Comments.Add(currentComment);
                var result = await repository.Update(target);
                var commentsWithUsers = await GetFullUserInComments(mapper.Map<IEnumerable<CommentDTO>>(result.Comments));
                await signalRService.CommentAdded($"{Group.complexString}{identifier}", identifier);

                return commentsWithUsers;
            }
            return null;

        }

        public async Task<IEnumerable<CommentDTO>> DeleteComment(int identifier, Guid commentId)
        {
            var target = await repository.GetAsync(identifier);
            if (target != null)
            {
                
                var currentComment = target.Comments.FirstOrDefault(x => x.Id == commentId);

                var comments = target.Comments;
                comments.Remove(currentComment);
                target.Comments = comments;
                
                var result = await repository.Update(target);
                var commentsWithUsers = await GetFullUserInComments(mapper.Map<IEnumerable<CommentDTO>>(result.Comments));
#warning пробросить событие удаления комментария
                //   await signalRService.CommentDeleted($"{Group.complexString}{identifier}", identifier);

                return commentsWithUsers;

            }
            return null;
        }

        public async Task<IEnumerable<CommentDTO>> EditComment(int identifier, CommentDTO comment)
        {
            var target = await repository.GetAsync(identifier);
            if (target != null)
            {
                var comments = mapper.Map<List<Comment>>(target.Comments);
                var currentComment = comments.FirstOrDefault(x => x.Id == comment.Id);
                
                currentComment.Text = comment.Text;
                currentComment.CreatedOn = DateTime.Now;;
                target.Comments = comments;

                var result = await repository.Update(target);
                var commentsWithUsers = await GetFullUserInComments(mapper.Map<IEnumerable<CommentDTO>>(result.Comments));
#warning пробросить событие изменения комментария
                //   await signalRService.CommentEddited($"{Group.complexString}{identifier}", identifier);
                return commentsWithUsers;
            }
            return null;

        }
        
        public async Task<IEnumerable<CommentDTO>> GetCommentsAsync(int identifier)
        {
            var target = await repository.GetAsync(identifier);
            if (target != null)
            {
                return await GetFullUserInComments(
                     mapper.Map<IEnumerable<CommentDTO>>(target.Comments));
            }

            return null;
        }

        private async Task<IEnumerable<CommentDTO>> GetFullUserInComments(IEnumerable<CommentDTO> comments)
        {
            foreach(var com in comments)
            {
                com.User = await userSevice.GetOneAsync(com.User.Id);
            }
            return comments;
        }

        public async Task<IEnumerable<HistoryDTO>> GetHistoryAsync(int identifier, Guid translationId)
        {
            var complexString = await GetComplexString(identifier);
            var translation = complexString.Translations.FirstOrDefault(t => t.Id == translationId);

            if (translation == null || complexString == null)
                return null;

            var history = new List<HistoryDTO>();

            if (translation.History.Count == 0)
            {
                var user = await userSevice.GetOneAsync(translation.UserId);
                history.Add(new HistoryDTO {
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
                var first = await userSevice.GetOneAsync(translation.History[0].UserId);
                history.Add(new HistoryDTO
                {
                    UserName = first.FullName,
                    AvatarUrl = first.AvatarUrl,
                    Action = "translated",
                    From = $"{complexString.OriginalValue}",
                    To = $"{translation.History[0].TranslationValue}",
                    When = translation.History[0].CreatedOn
                });

                for (int i = 1; i < translation.History.Count; i++)
                {
                    var user = await userSevice.GetOneAsync(translation.History[i].UserId);
                    history.Add(new HistoryDTO
                    {
                        UserName = user.FullName,
                        AvatarUrl = user.AvatarUrl,
                        Action = "changed",
                        From = $"{translation.History[i-1].TranslationValue}",
                        To = $"{translation.History[i].TranslationValue}",
                        When = translation.History[i].CreatedOn
                    });
                }

                var last = await userSevice.GetOneAsync(translation.UserId);
                history.Add(new HistoryDTO
                {
                    UserName = last.FullName,
                    AvatarUrl = last.AvatarUrl,
                    Action = "changed",
                    From = $"{translation.History[translation.History.Count - 1].TranslationValue}",
                    To = $"{translation.TranslationValue}",
                    When = translation.CreatedOn
                });
            }

            history.Reverse();

            return history;
        }

       
    }
}
