using AutoMapper;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.MongoModels;
using System.Collections.Generic;
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
                   .ForMember(p => p.ExplanationText, opt => opt.MapFrom(po => po.ExplanationText))
                   .ForMember(p => p.OriginLanguage, opt => opt.MapFrom(po => po.OriginLanguage))
                   .ForMember(p => p.ProjectGlossaries, opt => opt.MapFrom(po => po.ProjectGlossaries))
                   .ForMember(p => p.TermText, opt => opt.MapFrom(po => po.TermText));
                cfg.CreateMap<Glossary, GlossaryDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.ExplanationText, opt => opt.MapFrom(pt => pt.ExplanationText))
                    .ForMember(p => p.OriginLanguage, opt => opt.MapFrom(pt => pt.OriginLanguage))
                    .ForMember(p => p.ProjectGlossaries, opt => opt.MapFrom(pt => pt.ProjectGlossaries))
                    .ForMember(p => p.TermText, opt => opt.MapFrom(pt => pt.TermText));

                cfg.CreateMap<LanguageDTO, Language>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                   .ForMember(p => p.Code, opt => opt.MapFrom(po => po.Code))
                   .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name));
                cfg.CreateMap<Language, LanguageDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Code, opt => opt.MapFrom(pt => pt.Code))
                    .ForMember(p => p.Name, opt => opt.MapFrom(pt => pt.Name));

                cfg.CreateMap<ManagerDTO, Manager>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.UserProfile, opt => opt.MapFrom(po => po.UserProfile))
#warning нужны ли проекты?
                    .ForMember(p => p.Projects, opt => opt.Ignore());

                cfg.CreateMap<Manager, ManagerDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.UserProfile, opt => opt.MapFrom(pt => pt.UserProfile))
                    .ForMember(p => p.Projects, opt => opt.Ignore());

				cfg.CreateMap<ProjectDTO, Project>()
					.ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
					.ForMember(p => p.CreatedOn, opt => opt.MapFrom(po => po.CreatedOn))
					.ForMember(p => p.Description, opt => opt.MapFrom(po => po.Description))
					.ForMember(p => p.ImageUrl, opt => opt.MapFrom(po => po.ImageUrl))
					.ForMember(p => p.MainLanguage, opt => opt.MapFrom(po => po.MainLanguage))
					.ForMember(p => p.Manager, opt => opt.MapFrom(po => po.Manager))
#warning что нужно мапить?
                    .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name))				
                    .ForMember(p => p.ProjectGlossaries, opt => opt.MapFrom(po => po.ProjectGlossaries))
                    .ForMember(p => p.ProjectLanguageses, opt => opt.MapFrom(po => po.ProjectLanguageses))
                    .ForMember(p => p.ProjectTags, opt => opt.MapFrom(po => po.ProjectTags))
                    .ForMember(p => p.Teams, opt => opt.MapFrom(po => po.Teams))
                    .ForMember(p => p.Technology, opt => opt.MapFrom(po => po.Technology))
                    .ForMember(p => p.Translations, opt => opt.MapFrom(po => po.Translations));
					
				cfg.CreateMap<Project, ProjectDTO>()
					.ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
					.ForMember(p => p.CreatedOn, opt => opt.MapFrom(pt => pt.CreatedOn))
					.ForMember(p => p.Description, opt => opt.MapFrom(pt => pt.Description))
					.ForMember(p => p.ImageUrl, opt => opt.MapFrom(pt => pt.ImageUrl))
					.ForMember(p => p.MainLanguage, opt => opt.MapFrom(pt => pt.MainLanguage))
					.ForMember(p => p.Manager, opt => opt.Ignore())
					.ForMember(p => p.Name, opt => opt.MapFrom(pt => pt.Name))				
                    .ForMember(p => p.ProjectGlossaries, opt => opt.Ignore())
                    .ForMember(p => p.ProjectLanguageses, opt => opt.Ignore())
                    .ForMember(p => p.ProjectTags, opt => opt.Ignore())
                    .ForMember(p => p.Teams, opt => opt.Ignore())
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
                    .ForMember(p => p.Rate, opt => opt.MapFrom(po => po.Rate));
                cfg.CreateMap<Rating, RatingDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Comment, opt => opt.MapFrom(pt => pt.Comment))
                    .ForMember(p => p.CreatedAt, opt => opt.MapFrom(pt => pt.CreatedAt))
                    .ForMember(p => p.CreatedBy, opt => opt.MapFrom(pt => pt.CreatedBy))
                    .ForMember(p => p.Rate, opt => opt.MapFrom(pt => pt.Rate));

                cfg.CreateMap<RightDTO, Right>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Definition, opt => opt.MapFrom(po => po.Definition))
                    .ForMember(p => p.TranslatorRights, opt => opt.MapFrom(po => po.TranslatorRights));
                cfg.CreateMap<Right, RightDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Definition, opt => opt.MapFrom(pt => pt.Definition))
                    .ForMember(p => p.TranslatorRights, opt => opt.Ignore());

                cfg.CreateMap<TagDTO, Tag>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Color, opt => opt.MapFrom(po => po.Color))
                    .ForMember(p => p.Name, opt => opt.MapFrom(po => po.Name))
                    .ForMember(p => p.ProjectTags, opt => opt.MapFrom(po => po.ProjectTags));
                cfg.CreateMap<Tag, TagDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Color, opt => opt.MapFrom(pt => pt.Color))
                    .ForMember(p => p.Name, opt => opt.MapFrom(pt => pt.Name))
                    .ForMember(p => p.ProjectTags, opt => opt.MapFrom(pt => pt.ProjectTags));
               

                cfg.CreateMap<TeamDTO, Team>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.TeamTranslators, opt => opt.MapFrom(po => po.TeamTranslators));
                cfg.CreateMap<Team, TeamDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.TeamTranslators, opt => opt.Ignore());

         
                cfg.CreateMap<Polyglot.Common.DTOs.TranslationDTO, Polyglot.DataAccess.Entities.ComplexString>()
                    .ForMember(p => p.TranslationKey, opt => opt.MapFrom(pt => pt.TranslationKey));
                cfg.CreateMap<Polyglot.DataAccess.Entities.ComplexString, Polyglot.Common.DTOs.TranslationDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.TranslationKey, opt => opt.MapFrom(pt => pt.TranslationKey));

                cfg.CreateMap<TranslatorDTO, Translator>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Ratings, opt => opt.MapFrom(po => po.Ratings))
#warning нужны ли TeamTranslators?
                    .ForMember(p => p.TeamTranslators, opt => opt.MapFrom(po => po.TeamTranslators))
                    .ForMember(p => p.UserProfile, opt => opt.MapFrom(po => po.UserProfile));
                cfg.CreateMap<Translator, TranslatorDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                    .ForMember(p => p.Ratings, opt => opt.MapFrom(pt => pt.Ratings))
                    .ForMember(p => p.TeamTranslators, opt => opt.Ignore())
                    .ForMember(p => p.UserProfile, opt => opt.MapFrom(pt => pt.UserProfile));
                           
              

                cfg.CreateMap<Team, TeamPrevDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.Persons, opt => opt.MapFrom(po => 
                        po.TeamTranslators
                        .Select(t => t.Translator.UserProfile)));

                cfg.CreateMap<TeamTranslator, TeammateDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Translator.Id))
                    .ForMember(p => p.FullName, opt => opt.MapFrom(po => po.Translator.UserProfile.FullName))
                    .ForMember(p => p.TeamId, opt => opt.MapFrom(po => po.TeamId))
#warning примапить email
                    .ForMember(p => p.Email, opt => opt.UseValue("EMAIL_NOT_MAPPED_YET"))
                    .ForMember(p => p.Rights, opt => opt.MapFrom(po => po.TranslatorRights.Select(tr => tr.Right)));

                cfg.CreateMap<UserProfile, UserProfilePrevDTO>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                    .ForMember(p => p.AvatarUrl, opt => opt.MapFrom(po => po.AvatarUrl));

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
                    .ForMember(p => p.Uid, opt => opt.MapFrom(po => po.Uid));
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
                    .ForMember(p => p.Uid, opt => opt.MapFrom(pt => pt.Uid));

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
                  .ForMember(p => p.UserId, opt => opt.MapFrom(po => po.UserId));
                cfg.CreateMap<Comment, CommentDTO>()
                  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(pt => pt.CreatedOn))
                  .ForMember(p => p.Text, opt => opt.MapFrom(pt => pt.Text))
                  .ForMember(p => p.UserId, opt => opt.MapFrom(pt => pt.UserId));

				cfg.CreateMap<ComplexStringDTO, ComplexString>()
                  .ForMember(p => p.Id, opt => opt.MapFrom(po => po.Id))
                  .ForMember(p => p.Comments, opt => opt.MapFrom(po => po.Comments))
				  .ForMember(p => p.Description, opt => opt.MapFrom(po => po.Description))
				  .ForMember(p => p.Language, opt => opt.MapFrom(po => po.Language))
				  .ForMember(p => p.OriginalValue, opt => opt.MapFrom(po => po.OriginalValue))
				  .ForMember(p => p.PictureLink, opt => opt.MapFrom(po => po.PictureLink))
				  .ForMember(p => p.ProjectId, opt => opt.MapFrom(po => po.ProjectId))
				  .ForMember(p => p.Tags, opt => opt.MapFrom(po => po.Tags))
				  .ForMember(p => p.Translations, opt => opt.MapFrom(po => po.Translations))
				  .ForMember(p => p.Key, opt => opt.MapFrom(po => po.Key));
                cfg.CreateMap<ComplexString, ComplexStringDTO>()
                  .ForMember(p => p.Id, opt => opt.MapFrom(pt => pt.Id))
                  .ForMember(p => p.Comments, opt => opt.MapFrom(po => po.Comments))
                  .ForMember(p => p.Description, opt => opt.MapFrom(po => po.Description))
                  .ForMember(p => p.Language, opt => opt.MapFrom(po => po.Language))
                  .ForMember(p => p.OriginalValue, opt => opt.MapFrom(po => po.OriginalValue))
                  .ForMember(p => p.PictureLink, opt => opt.MapFrom(po => po.PictureLink))
                  .ForMember(p => p.ProjectId, opt => opt.MapFrom(po => po.ProjectId))
                  .ForMember(p => p.Tags, opt => opt.MapFrom(po => po.Tags))
                  .ForMember(p => p.Translations, opt => opt.MapFrom(po => po.Translations));

                cfg.CreateMap<Polyglot.Common.DTOs.NoSQL.TranslationDTO, Polyglot.DataAccess.MongoModels.Translation>()
                  .ForMember(p => p.History, opt => opt.MapFrom(po => po.History))
                  .ForMember(p => p.Language, opt => opt.MapFrom(po => po.Language))
                  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(po => po.CreatedOn))
                  .ForMember(p => p.OptionalTranslations, opt => opt.MapFrom(po => po.OptionalTranslations))
                  .ForMember(p => p.TranslationValue, opt => opt.MapFrom(po => po.TranslationValue))
                  .ForMember(p => p.UserId, opt => opt.MapFrom(po => po.UserId));
                cfg.CreateMap<Polyglot.DataAccess.MongoModels.Translation, Polyglot.Common.DTOs.NoSQL.TranslationDTO>()
                  .ForMember(p => p.History, opt => opt.MapFrom(pt => pt.History))
                  .ForMember(p => p.Language, opt => opt.MapFrom(pt => pt.Language))
                  .ForMember(p => p.CreatedOn, opt => opt.MapFrom(pt => pt.CreatedOn))
                  .ForMember(p => p.OptionalTranslations, opt => opt.MapFrom(pt => pt.OptionalTranslations))
                  .ForMember(p => p.TranslationValue, opt => opt.MapFrom(pt => pt.TranslationValue))
                  .ForMember(p => p.UserId, opt => opt.MapFrom(pt => pt.UserId));

                #endregion

            }).CreateMapper();
        }
    }
}
