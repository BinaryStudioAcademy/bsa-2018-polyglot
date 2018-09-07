using AutoMapper;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.Chat;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Entities.Chat;
using Polyglot.DataAccess.MongoModels;
using System.Linq;
using ComplexString = Polyglot.DataAccess.MongoModels.ComplexString;

namespace Polyglot.Common.Mapping
{
    public class AutoMapper
    {
        public static IMapper GetDefaultMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                #region SQL

                cfg.CreateMap<FileDTO, File>()
                   .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                   .ForMember(p => p.Link, opt => opt.MapFrom(po => po.Link))
                   .ForMember(p => p.Project, opt => opt.MapFrom(po => po.Project))
                   .ForMember(p => p.CreatedOn, opt => opt.MapFrom(po => po.CreatedOn))
                   .ForMember(p => p.UploadedBy, opt => opt.MapFrom(po => po.UploadedBy));
                cfg.CreateMap<File, FileDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Link, opt => opt.MapFrom(pt => pt.Link))
                    .ForMember(p => p.Project, opt => opt.MapFrom(pt => pt.Project))
                    .ForMember(p => p.CreatedOn, opt => opt.MapFrom(pt => pt.CreatedOn))
                    .ForMember(p => p.UploadedBy, opt => opt.MapFrom(pt => pt.UploadedBy));

                cfg.CreateMap<GlossaryDTO, Glossary>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                   .ForMember(p => p.GlossaryStrings, opt => opt.MapFrom(po => po.GlossaryStrings))
                   .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name))
                   .ForMember(p => p.OriginLanguage, opt => opt.MapFrom(po => po.OriginLanguage))
                   .ForMember(p => p.ProjectGlossaries, opt => opt.MapFrom(po => po.ProjectGlossaries));
                cfg.CreateMap<Glossary, GlossaryDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.GlossaryStrings, opt => opt.MapFrom(pt => pt.GlossaryStrings))
                    .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name))
                    .ForMember(p => p.OriginLanguage, opt => opt.MapFrom(pt => pt.OriginLanguage))
                    .ForMember(p => p.ProjectGlossaries, opt => opt.Ignore());

                cfg.CreateMap<GlossaryStringDTO, GlossaryString>()
                   .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                   .ForMember(p => p.TermText, opt => opt.MapFrom(po => po.TermText))
                   .ForMember(p => p.ExplanationText, opt => opt.MapFrom(po => po.ExplanationText));
                cfg.CreateMap<GlossaryString, GlossaryStringDTO>()
                   .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                   .ForMember(p => p.TermText, opt => opt.MapFrom(po => po.TermText))
                   .ForMember(p => p.ExplanationText, opt => opt.MapFrom(po => po.ExplanationText));

                cfg.CreateMap<LanguageStatisticDTO, Language>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                   .ForMember(p => p.Code, opt => opt.MapFrom(po => po.Code))
                   .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name));
                cfg.CreateMap<Language, LanguageStatisticDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Code, opt => opt.MapFrom(pt => pt.Code))
                    .ForMember(p => p.Name, opt => opt.MapFrom(pt => pt.Name))
                    .ForMember(p => p.TranslatedStringsCount, opt => opt.Ignore())
                    .ForMember(p => p.ComplexStringsCount, opt => opt.Ignore());

                cfg.CreateMap<LanguageDTO, Language>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                   .ForMember(p => p.Code, opt => opt.MapFrom(po => po.Code))
                   .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name));
                cfg.CreateMap<Language, LanguageDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Code, opt => opt.MapFrom(pt => pt.Code))
                    .ForMember(p => p.Name, opt => opt.MapFrom(pt => pt.Name));

                cfg.CreateMap<ProjectDTO, Project>()
					.ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
					.ForMember(p => p.CreatedOn, opt => opt.MapFrom(po => po.CreatedOn))
					.ForMember(p => p.Description, opt => opt.MapFrom(po => po.Description))
					.ForMember(p => p.ImageUrl, opt => opt.MapFrom(po => po.ImageUrl))
					.ForMember(p => p.MainLanguage, opt => opt.MapFrom(po => po.MainLanguage))
					.ForMember(p => p.UserProfile, opt => opt.MapFrom(po => po.UserProfile))
                    .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name))				
                    .ForMember(p => p.ProjectGlossaries, opt => opt.MapFrom(po => po.ProjectGlossaries))
                    .ForMember(p => p.ProjectLanguageses, opt => opt.Ignore())
                    .ForMember(p => p.ProjectTeams, opt => opt.MapFrom(po => po.ProjectTeams))
                    .ForMember(p => p.Technology, opt => opt.MapFrom(po => po.Technology))
                    .ForMember(p => p.Translations, opt => opt.MapFrom(po => po.Translations));
					
				cfg.CreateMap<Project, ProjectDTO>()
					.ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
					.ForMember(p => p.CreatedOn, opt => opt.MapFrom(pt => pt.CreatedOn))
					.ForMember(p => p.Description, opt => opt.MapFrom(pt => pt.Description))
					.ForMember(p => p.ImageUrl, opt => opt.MapFrom(pt => pt.ImageUrl))
					.ForMember(p => p.MainLanguage, opt => opt.MapFrom(pt => pt.MainLanguage))
                    .ForMember(p => p.UserProfile, opt => opt.MapFrom(pt => pt.UserProfile))
					.ForMember(p => p.Name, opt => opt.MapFrom(pt => pt.Name))				
                    .ForMember(p => p.ProjectGlossaries, opt => opt.Ignore())
                    .ForMember(p => p.ProjectLanguageses, opt => opt.MapFrom(pt => pt.ProjectLanguageses.Select(l => l.Language)))
                    .ForMember(p => p.Tags, opt => opt.MapFrom(pt => pt.Tags))
                    .ForMember(p => p.ProjectTeams, opt => opt.Ignore())
                    .ForMember(p => p.Technology, opt => opt.MapFrom(pt => pt.Technology))
                    .ForMember(p => p.Translations, opt => opt.Ignore());
					            

                cfg.CreateMap<ProjectHistoryDTO, ProjectHistory>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.ActionType, opt => opt.MapFrom(po => po.ActionType))
                    .ForMember(p => p.Author, opt => opt.MapFrom(po => po.Actor))
                    .ForMember(p => p.OriginValue, opt => opt.MapFrom(po => po.OriginValue))
                    .ForMember(p => p.Project, opt => opt.MapFrom(po => po.Project))
                    .ForMember(p => p.TableName, opt => opt.MapFrom(po => po.TableName))
                    .ForMember(p => p.Time, opt => opt.MapFrom(po => po.Time));
                cfg.CreateMap<ProjectHistory, ProjectHistoryDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.ActionType, opt => opt.MapFrom(pt => pt.ActionType))
                    .ForMember(p => p.Actor, opt => opt.MapFrom(pt => pt.Author))
                    .ForMember(p => p.OriginValue, opt => opt.MapFrom(pt => pt.OriginValue))
                    .ForMember(p => p.Project, opt => opt.MapFrom(pt => pt.Project))
                    .ForMember(p => p.TableName, opt => opt.MapFrom(pt => pt.TableName))
                    .ForMember(p => p.Time, opt => opt.MapFrom(pt => pt.Time));
                
                cfg.CreateMap<RatingDTO, Rating>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Comment, opt => opt.MapFrom(po => po.Comment))
                    .ForMember(p => p.CreatedAt, opt => opt.MapFrom(po => po.CreatedAt))
                    .ForMember(p => p.CreatedBy, opt => opt.MapFrom(po => po.CreatedBy))
                    .ForMember(p => p.CreatedById, opt => opt.MapFrom(po => po.CreatedById))
                    .ForMember(p => p.User, opt => opt.MapFrom(po => po.User))
                    .ForMember(p => p.UserId, opt => opt.MapFrom(po => po.UserId))
                    .ForMember(p => p.Rate, opt => opt.MapFrom(po => po.Rate));
                cfg.CreateMap<Rating, RatingDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Comment, opt => opt.MapFrom(pt => pt.Comment))
                    .ForMember(p => p.CreatedAt, opt => opt.MapFrom(pt => pt.CreatedAt))
                    .ForMember(p => p.CreatedBy, opt => opt.MapFrom(po => po.CreatedBy))
                    .ForMember(p => p.CreatedById, opt => opt.MapFrom(po => po.CreatedById))
                    .ForMember(p => p.User, opt => opt.MapFrom(po => po.User))
                    .ForMember(p => p.UserId, opt => opt.MapFrom(po => po.UserId))
                    .ForMember(p => p.Rate, opt => opt.MapFrom(pt => pt.Rate));

                cfg.CreateMap<RightDTO, Right>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Definition, opt => opt.MapFrom(po => po.Definition));
                cfg.CreateMap<Right, RightDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Definition, opt => opt.MapFrom(pt => (int)pt.Definition));

                cfg.CreateMap<TagDTO, Tag>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Color, opt => opt.MapFrom(po => po.Color))
                    .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name));
                cfg.CreateMap<Tag, TagDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Color, opt => opt.MapFrom(pt => pt.Color))
                    .ForMember(p => p.Name, opt => opt.MapFrom(pt => pt.Name));


                cfg.CreateMap<TeamDTO, Team>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.TeamTranslators, opt => opt.MapFrom(p => p.TeamTranslators))
                    .ForMember(p => p.ProjectTeams, opt => opt.MapFrom(p => p.TeamProjects))
                    .ForMember(p => p.CreatedBy, opt => opt.MapFrom(p => p.CreatedBy))
                    .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name));
                cfg.CreateMap<Team, TeamDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.TeamTranslators, opt => opt.MapFrom(p => p.TeamTranslators))
                    .ForMember(p => p.TeamProjects, opt => opt.MapFrom(p => p.ProjectTeams))
                    .ForMember(p => p.CreatedBy, opt => opt.MapFrom(p => p.CreatedBy))
                    .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name));

                cfg.CreateMap<TranslatorLanguage, TranslatorLanguageDTO>()
                    .ForMember(p => p.Language, opt => opt.MapFrom(po => po.Language))
#warning после обновления типа Proficiency примапить
                    //.ForMember(p => p.Proficiency, opt => opt.MapFrom(po => (int)po.Proficiency));
                    .ForMember(p => p.Proficiency, opt => opt.Ignore());


                cfg.CreateMap<TeamTranslator, TranslatorDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.FullName, opt => opt.MapFrom(po => po.UserProfile.FullName))
                    .ForMember(p => p.AvatarUrl, opt => opt.MapFrom(po => po.UserProfile.AvatarUrl))
                    .ForMember(p => p.TeamId, opt => opt.MapFrom(po => po.TeamId))
                    .ForMember(p => p.UserId, opt => opt.MapFrom(po => po.TranslatorId))
#warning примапить email
                    .ForMember(p => p.Email, opt => opt.UseValue("EMAIL_NOT_MAPPED_YET"))
                    .ForMember(p => p.Rating, opt => opt.Ignore())
                    .ForMember(p => p.TranslatorLanguages, opt => opt.Ignore())
                    .ForMember(p => p.Rights, opt => opt.MapFrom(po => po.TranslatorRights.Select(tr => tr.Right)));
                cfg.CreateMap<TranslatorDTO, TeamTranslator>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.TeamId, opt => opt.MapFrom(po => po.TeamId))
                    .ForMember(p => p.TranslatorId, opt => opt.MapFrom(po => po.UserId));

                cfg.CreateMap<UserProfile, TranslatorDTO>()
                    .ForMember(p => p.UserId, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.FullName, opt => opt.MapFrom(po => po.FullName))
                    .ForMember(p => p.AvatarUrl, opt => opt.MapFrom(po => po.AvatarUrl))
                    .ForMember(p => p.Id, opt => opt.Ignore())
                    .ForMember(p => p.TeamId, opt => opt.Ignore())
#warning примапить email
                    .ForMember(p => p.Email, opt => opt.UseValue("EMAIL_NOT_MAPPED_YET"))
                    .ForMember(p => p.Rating, opt => opt.Ignore())
                    .ForMember(p => p.Rights, opt => opt.Ignore());

                cfg.CreateMap<Polyglot.Common.DTOs.TranslationDTO, Polyglot.DataAccess.Entities.ComplexString>()
                    .ForMember(p => p.TranslationKey, opt => opt.MapFrom(pt => pt.TranslationKey));
                cfg.CreateMap<Polyglot.DataAccess.Entities.ComplexString, Polyglot.Common.DTOs.TranslationDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.TranslationKey, opt => opt.MapFrom(pt => pt.TranslationKey));


                cfg.CreateMap<Team, TeamPrevDTO>()
                    .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name))
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name))
                    .ForMember(p => p.CreatedBy, opt => opt.MapFrom(p => p.CreatedBy))
                    .ForMember(p => p.Persons, opt => opt.MapFrom(po => 
                        po.TeamTranslators
                        .Select(t => t.UserProfile)));

                cfg.CreateMap<UserProfile, UserProfilePrevDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.AvatarUrl, opt => opt.MapFrom(po => po.AvatarUrl));

                cfg.CreateMap<UserProfile, ChatUserDTO>()
                .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                .ForMember(p => p.Email, opt => opt.UseValue("NOT MAPPED"))
                .ForMember(p => p.FullName, opt => opt.MapFrom(po => po.FullName))
                .ForMember(p => p.Role, opt => opt.MapFrom(po => po.UserRole))
                .ForMember(p => p.AvatarUrl, opt => opt.MapFrom(po => po.AvatarUrl));

                cfg.CreateMap<ChatMessageDTO, ChatMessage>()
                .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                .ForMember(p => p.Body, opt => opt.MapFrom(po => po.Body))
                .ForMember(p => p.IsRead, opt => opt.MapFrom(po => po.IsRead))
                .ForMember(p => p.ReceivedDate, opt => opt.MapFrom(po => po.ReceivedDate))
                .ForMember(p => p.Dialog, opt => opt.Ignore())
                .ForMember(p => p.DialogId, opt => opt.MapFrom(po => po.DialogId))
                .ForMember(p => p.Sender, opt => opt.Ignore())
                .ForMember(p => p.SenderId, opt => opt.MapFrom(po => po.SenderId));

                cfg.CreateMap<ChatMessage, ChatMessageDTO>()
                .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                .ForMember(p => p.Body, opt => opt.MapFrom(po => po.Body))
                .ForMember(p => p.IsRead, opt => opt.MapFrom(po => po.IsRead))
                .ForMember(p => p.ReceivedDate, opt => opt.MapFrom(po => po.ReceivedDate))
                .ForMember(p => p.DialogId, opt => opt.MapFrom(po => po.DialogId))
                .ForMember(p => p.SenderId, opt => opt.MapFrom(po => po.SenderId));

                cfg.CreateMap<ChatDialog, ChatDialogDTO>()
                .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                .ForMember(p => p.DialogType, opt => opt.MapFrom(po => po.DialogType))
                .ForMember(p => p.Identifier, opt => opt.MapFrom(po => po.Identifier))
                .ForMember(p => p.DialogName, opt => opt.MapFrom(po => po.DialogName))
                .ForMember(p => p.Participants, opt => opt.MapFrom(po => po.DialogParticipants.Select(dp => dp.Participant)));

                cfg.CreateMap<ChatDialogDTO, ChatDialog>()
                .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                .ForMember(p => p.DialogType, opt => opt.MapFrom(po => po.DialogType))
                .ForMember(p => p.Identifier, opt => opt.MapFrom(po => po.Identifier))
                .ForMember(p => p.DialogName, opt => opt.MapFrom(po => po.DialogName))
                .ForMember(p => p.DialogParticipants, opt => opt.Ignore())
                .ForMember(p => p.Messages, opt => opt.Ignore());


                cfg.CreateMap<UserProfileDTO, UserProfile>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Address, opt => opt.MapFrom(po => po.Address))
                    .ForMember(p => p.AvatarUrl, opt => opt.MapFrom(po => po.AvatarUrl))
                    .ForMember(p => p.BirthDate, opt => opt.MapFrom(po => po.BirthDate))
                    .ForMember(p => p.City, opt => opt.MapFrom(po => po.City))
                    .ForMember(p => p.Country, opt => opt.MapFrom(po => po.Country))
                    .ForMember(p => p.Phone, opt => opt.MapFrom(po => po.Phone))
                    .ForMember(p => p.PostalCode, opt => opt.MapFrom(po => po.PostalCode))
                    .ForMember(p => p.Region, opt => opt.MapFrom(po => po.Region))
                    .ForMember(p => p.RegistrationDate, opt => opt.MapFrom(po => po.RegistrationDate))
                    .ForMember(p => p.FullName, opt => opt.MapFrom(po => po.FullName))
                    .ForMember(p => p.Uid, opt => opt.MapFrom(po => po.Uid))
                    .ForMember(p => p.Ratings, opt => opt.Ignore())
                    .ForMember(p => p.TeamTranslators, opt => opt.Ignore())
                    .ForMember(p => p.UserRole, opt => opt.MapFrom(po => po.UserRole))
                    .ForMember(p => p.Projects, opt => opt.Ignore());
                cfg.CreateMap<UserProfile, UserProfileDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Address, opt => opt.MapFrom(pt => pt.Address))
                    .ForMember(p => p.AvatarUrl, opt => opt.MapFrom(pt => pt.AvatarUrl))
                    .ForMember(p => p.BirthDate, opt => opt.MapFrom(pt => pt.BirthDate))
                    .ForMember(p => p.City, opt => opt.MapFrom(pt => pt.City))
                    .ForMember(p => p.Country, opt => opt.MapFrom(pt => pt.Country))
                    .ForMember(p => p.Phone, opt => opt.MapFrom(pt => pt.Phone))
                    .ForMember(p => p.PostalCode, opt => opt.MapFrom(pt => pt.PostalCode))
                    .ForMember(p => p.Region, opt => opt.MapFrom(pt => pt.Region))
                    .ForMember(p => p.RegistrationDate, opt => opt.MapFrom(pt => pt.RegistrationDate))
                    .ForMember(p => p.FullName, opt => opt.MapFrom(pt => pt.FullName))
                    .ForMember(p => p.Uid, opt => opt.MapFrom(pt => pt.Uid))
                    .ForMember(p => p.Ratings, opt => opt.Ignore())
                    .ForMember(p => p.TeamTranslators, opt => opt.Ignore())
                    .ForMember(p => p.UserRole, opt => opt.MapFrom(pt => (int)pt.UserRole))
                    .ForMember(p => p.Projects, opt => opt.Ignore());

                cfg.CreateMap<Notification, NotificationDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Sender, opt => opt.MapFrom(po => po.Sender))
                    .ForMember(p => p.Sender, opt => opt.MapFrom(po => po.Sender))
                    .ForMember(p => p.Receiver, opt => opt.MapFrom(po => po.Receiver))
                    .ForMember(p => p.Message, opt => opt.MapFrom(po => po.Message))
                    .ForMember(p => p.Options, opt => opt.MapFrom(po => po.Options))
                    .ForMember(p => p.ReceiverId, opt => opt.MapFrom(po => po.ReceiverId))
                    .ForMember(p => p.SenderId, opt => opt.MapFrom(po => po.SenderId))
                    .ForMember(p => p.NotificationAction, opt => opt.MapFrom(po => po.NotificationAction))
                    .ForMember(p => p.Payload, opt => opt.MapFrom(po => po.Payload));

                cfg.CreateMap<NotificationDTO, Notification>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Sender, opt => opt.MapFrom(po => po.Sender))
                    .ForMember(p => p.Receiver, opt => opt.MapFrom(po => po.Receiver))
                    .ForMember(p => p.Message, opt => opt.MapFrom(po => po.Message))
                    .ForMember(p => p.Options, opt => opt.MapFrom(po => po.Options))
                    .ForMember(p => p.ReceiverId, opt => opt.MapFrom(po => po.ReceiverId))
                    .ForMember(p => p.SenderId, opt => opt.MapFrom(po => po.SenderId))
                    .ForMember(p => p.NotificationAction, opt => opt.MapFrom(po => po.NotificationAction))
                    .ForMember(p => p.Payload, opt => opt.MapFrom(po => po.Payload));

                cfg.CreateMap<OptionDTO, Option>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.NotificationId, opt => opt.MapFrom(po => po.NotificationId))
                    .ForMember(p => p.OptionDefinition, opt => opt.MapFrom(po => po.OptionDefinition));

                cfg.CreateMap<Option, OptionDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.NotificationId, opt => opt.MapFrom(po => po.NotificationId))
                    .ForMember(p => p.OptionDefinition, opt => opt.MapFrom(po => po.OptionDefinition));

                cfg.CreateMap<ProjectTeam, TeamProjectDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Project, opt => opt.MapFrom(po => po.Project))
                    .ForMember(p => p.ProjectId, opt => opt.MapFrom(po => po.ProjectId))
                    .ForMember(p => p.TeamId, opt => opt.MapFrom(po => po.TeamId));

                cfg.CreateMap<TeamProjectDTO, ProjectTeam>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Project, opt => opt.MapFrom(po => po.Project))
                    .ForMember(p => p.ProjectId, opt => opt.MapFrom(po => po.ProjectId))
                    .ForMember(p => p.TeamId, opt => opt.MapFrom(po => po.TeamId));

                #endregion

                #region NoSQL

                cfg.CreateMap<AdditionalTranslationDTO, AdditionalTranslation>()
				  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(po => po.CreatedOn))
				  .ForMember(p => p.TranslationValue, opt => opt.MapFrom(po => po.TranslationValue))
				  .ForMember(p => p.UserId, opt => opt.MapFrom(po => po.UserId));
				cfg.CreateMap<AdditionalTranslation, AdditionalTranslationDTO>()
				  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(pt => pt.CreatedOn))
				  .ForMember(p => p.TranslationValue, opt => opt.MapFrom(pt => pt.TranslationValue))
				  .ForMember(p => p.UserId, opt => opt.MapFrom(pt => pt.UserId));

                cfg.CreateMap<CommentDTO, Comment>()
                  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(po => po.CreatedOn))
                  .ForMember(p => p.Text, opt => opt.MapFrom(po => po.Text))
                  .ForMember(p => p.UserId, opt => opt.MapFrom(po => po.User.Id));
                cfg.CreateMap<Comment, CommentDTO>()
                  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(pt => pt.CreatedOn))
                  .ForMember(p => p.Text, opt => opt.MapFrom(pt => pt.Text))
                  .ForMember(p => p.User, opt => opt.MapFrom(pt => new UserProfile {
                      Id = pt.UserId                      
                  }));

				cfg.CreateMap<ComplexStringDTO, ComplexString>()
                  .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                  .ForMember(p => p.Comments, opt => opt.MapFrom(po => po.Comments))
				  .ForMember(p => p.Description, opt => opt.MapFrom(po => po.Description))
				  .ForMember(p => p.LanguageId, opt => opt.MapFrom(po => po.LanguageId))
				  .ForMember(p => p.OriginalValue, opt => opt.MapFrom(po => po.OriginalValue))
				  .ForMember(p => p.PictureLink, opt => opt.MapFrom(po => po.PictureLink))
				  .ForMember(p => p.ProjectId, opt => opt.MapFrom(po => po.ProjectId))
				  .ForMember(p => p.Tags, opt => opt.Ignore())
				  .ForMember(p => p.Translations, opt => opt.MapFrom(po => po.Translations))
				  .ForMember(p => p.Key, opt => opt.MapFrom(po => po.Key))
                  .ForMember(p => p.CreatedBy, opt => opt.MapFrom(po => po.CreatedBy))
				  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(po => po.CreatedOn));


                cfg.CreateMap<ComplexString, ComplexStringDTO>()
                  .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                  .ForMember(p => p.Comments, opt => opt.Ignore())
                  .ForMember(p => p.Description, opt => opt.MapFrom(po => po.Description))
                  .ForMember(p => p.LanguageId, opt => opt.MapFrom(po => po.LanguageId))
                  .ForMember(p => p.OriginalValue, opt => opt.MapFrom(po => po.OriginalValue))
                  .ForMember(p => p.PictureLink, opt => opt.MapFrom(po => po.PictureLink))
                  .ForMember(p => p.ProjectId, opt => opt.MapFrom(po => po.ProjectId))
                  .ForMember(p => p.Tags, opt => opt.Ignore())
                  .ForMember(p => p.Translations, opt => opt.MapFrom(po => po.Translations))
                  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(po => po.CreatedOn))
                  .ForMember(p => p.CreatedBy, opt => opt.MapFrom(po => po.CreatedBy));

                cfg.CreateMap<Polyglot.Common.DTOs.NoSQL.TranslationDTO, Polyglot.DataAccess.MongoModels.Translation>()
                  .ForMember(p => p.History, opt => opt.MapFrom(po => po.History))
                  .ForMember(p => p.LanguageId, opt => opt.MapFrom(po => po.LanguageId))
                  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(po => po.CreatedOn))
                  .ForMember(p => p.OptionalTranslations, opt => opt.MapFrom(po => po.OptionalTranslations))
                  .ForMember(p => p.TranslationValue, opt => opt.MapFrom(po => po.TranslationValue))
                  .ForMember(p => p.UserId, opt => opt.MapFrom(po => po.UserId))
                  .ForMember(p => p.AssignedTranslatorId, opt => opt.MapFrom(po => po.AssignedTranslatorId));

                cfg.CreateMap<Polyglot.DataAccess.MongoModels.Translation, Polyglot.Common.DTOs.NoSQL.TranslationDTO>()
                  .ForMember(p => p.History, opt => opt.MapFrom(pt => pt.History))
                  .ForMember(p => p.LanguageId, opt => opt.MapFrom(pt => pt.LanguageId))
                  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(pt => pt.CreatedOn))
                  .ForMember(p => p.OptionalTranslations, opt => opt.MapFrom(pt => pt.OptionalTranslations))
                  .ForMember(p => p.TranslationValue, opt => opt.MapFrom(pt => pt.TranslationValue))
                  .ForMember(p => p.UserId, opt => opt.MapFrom(pt => pt.UserId))
                  .ForMember(p => p.AssignedTranslatorId, opt => opt.MapFrom(pt => pt.AssignedTranslatorId));

                #endregion

            }).CreateMapper();
        }
    }
}
