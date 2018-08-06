using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Repositories;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
		private IRepository<File> _fileRepository;
		private IRepository<Glossary> _glossaryRepository;
		private IRepository<Language> _languageRepository;
		private IRepository<Manager> _managerRepository;
		private IRepository<Project> _projectRepository;
		private IRepository<Rating> _ratingRepository;
		private IRepository<Right> _rightRepository;
		private IRepository<Tag> _tagRepository;
		private IRepository<Team> _teamRepository;
		private IRepository<Translation> _translationRepository;
		private IRepository<Translator> _translatorRepository;
		private IRepository<UserProfile> _userProfileRepository;

		private DataContext _context;

		public UnitOfWork(DataContext c)
		{
			_context = c;
		}

		public IRepository<File> FileRepository
		{
			get
			{
				if (_fileRepository == null)
					_fileRepository = new Repository<File>(_context);

				return _fileRepository;
			}
		}

		public IRepository<Glossary> GlossaryRepository
		{
			get
			{
				if (_glossaryRepository == null)
					_glossaryRepository = new Repository<Glossary>(_context);

				return _glossaryRepository;
			}
		}

		public IRepository<Language> LanguageRepository
		{
			get
			{
				if (_languageRepository == null)
					_languageRepository = new Repository<Language>(_context);

				return _languageRepository;
			}
		}

		public IRepository<Manager> ManagerRepository
		{
			get
			{
				if (_managerRepository == null)
					_managerRepository = new Repository<Manager>(_context);

				return _managerRepository;
			}
		}

		public IRepository<Project> ProjectRepository
		{
			get
			{
				if (_projectRepository == null)
					_projectRepository = new Repository<Project>(_context);

				return _projectRepository;
			}
		}

		public IRepository<Rating> RatingRepository
		{
			get
			{
				if (_ratingRepository == null)
					_ratingRepository = new Repository<Rating>(_context);

				return _ratingRepository;
			}
		}

		public IRepository<Right> RightRepository
		{
			get
			{
				if (_rightRepository == null)
					_rightRepository = new Repository<Right>(_context);

				return _rightRepository;
			}
		}

		public IRepository<Tag> TagRepository
		{
			get
			{
				if (_tagRepository == null)
					_tagRepository = new Repository<Tag>(_context);

				return _tagRepository;
			}
		}

		public IRepository<Team> TeamRepository
		{
			get
			{
				if (_teamRepository == null)
					_teamRepository = new Repository<Team>(_context);

				return _teamRepository;
			}
		}

		public IRepository<Translation> TranslationRepository
		{
			get
			{
				if (_translationRepository == null)
					_translationRepository = new Repository<Translation>(_context);

				return _translationRepository;
			}
		}

		public IRepository<Translator> TranslatorRepository
		{
			get
			{
				if (_translatorRepository == null)
					_translatorRepository = new Repository<Translator>(_context);

				return _translatorRepository;
			}
		}

		public IRepository<UserProfile> UserProfileRepository
		{
			get
			{
				if (_userProfileRepository == null)
					_userProfileRepository = new Repository<UserProfile>(_context);

				return _userProfileRepository;
			}
		}
	}
}
