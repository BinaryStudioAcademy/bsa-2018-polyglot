using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
		IFileRepository FileRepository { get; }
		IGlossaryRepository GlossaryRepository { get; }
		ILanguageRepository LanguageRepository { get; }
		IManagerRepository ManagerRepository { get; }
		IProjectRepository ProjectRepository { get; }
		IRatingRepository RatingRepository { get; }
		IRightRepository RightRepository { get; }
		ITagRepository TagRepository { get; }
		ITeamRepository TeamRepository { get; }
		ITranslationRepository TranslationRepository { get; }
		ITranslatorRepository TranslatorRepository { get; }
		IUserProfileRepository UserProfileRepository { get; }
	}
}
