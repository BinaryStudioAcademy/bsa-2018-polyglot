using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Repositories;

namespace Polyglot.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
		private IFileRepository _fileRepository;
		private IGlossaryRepository _glossaryRepository;
		private ILanguageRepository _languageRepository;
		private IManagerRepository _managerRepository;
		private IProjectRepository _projectRepository;
		private IRatingRepository _ratingRepository;
		private IRightRepository _rightRepository;
		private ITagRepository _tagRepository;
		private ITeamRepository _teamRepository;
		private ITranslationRepository _translationRepository;
		private ITranslatorRepository _translatorRepository;
		private IUserProfileRepository _userProfileRepository;

		private DataContext _context;

		public UnitOfWork(DataContext c)
		{
			_context = c;
		}

		public IFileRepository FileRepository
		{
			get
			{
				if (_fileRepository == null)
					_fileRepository = new FileRepository(_context);

				return _fileRepository;
			}
		}

		public IGlossaryRepository GlossaryRepository
		{
			get
			{
				if (_glossaryRepository == null)
					_glossaryRepository = new GlossaryRepository(_context);

				return _glossaryRepository;
			}
		}

		public ILanguageRepository LanguageRepository
		{
			get
			{
				if (_languageRepository == null)
					_languageRepository = new LanguageRepository(_context);

				return _languageRepository;
			}
		}

		public IManagerRepository ManagerRepository
		{
			get
			{
				if (_managerRepository == null)
					_managerRepository = new ManagerRepository(_context);

				return _managerRepository;
			}
		}

		public IProjectRepository ProjectRepository
		{
			get
			{
				if (_projectRepository == null)
					_projectRepository = new ProjectRepository(_context);

				return _projectRepository;
			}
		}

		public IRatingRepository RatingRepository
		{
			get
			{
				if (_ratingRepository == null)
					_ratingRepository = new RatingRepository(_context);

				return _ratingRepository;
			}
		}

		public IRightRepository RightRepository
		{
			get
			{
				if (_rightRepository == null)
					_rightRepository = new RightRepository(_context);

				return _rightRepository;
			}
		}

		public ITagRepository TagRepository
		{
			get
			{
				if (_tagRepository == null)
					_tagRepository = new TagRepository(_context);

				return _tagRepository;
			}
		}

		public ITeamRepository TeamRepository
		{
			get
			{
				if (_teamRepository == null)
					_teamRepository = new TeamRepository(_context);

				return _teamRepository;
			}
		}

		public ITranslationRepository TranslationRepository
		{
			get
			{
				if (_translationRepository == null)
					_translationRepository = new TranslationRepository(_context);

				return _translationRepository;
			}
		}

		public ITranslatorRepository TranslatorRepository
		{
			get
			{
				if (_translatorRepository == null)
					_translatorRepository = new TranslatorRepository(_context);

				return _translatorRepository;
			}
		}

		public IUserProfileRepository UserProfileRepository
		{
			get
			{
				if (_userProfileRepository == null)
					_userProfileRepository = new UserProfileRepository(_context);

				return _userProfileRepository;
			}
		}
	}
}
