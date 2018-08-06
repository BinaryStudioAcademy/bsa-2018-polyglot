using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
		IRepository<File> FileRepository { get; }
		IRepository<Glossary> GlossaryRepository { get; }
		IRepository<Language> LanguageRepository { get; }
		IRepository<Manager> ManagerRepository { get; }
		IRepository<Project> ProjectRepository { get; }
		IRepository<Rating> RatingRepository { get; }
		IRepository<Right> RightRepository { get; }
		IRepository<Tag> TagRepository { get; }
		IRepository<Team> TeamRepository { get; }
		IRepository<Translation> TranslationRepository { get; }
		IRepository<Translator> TranslatorRepository { get; }
		IRepository<UserProfile> UserProfileRepository { get; }
		Task SaveAsync();
	}
}
