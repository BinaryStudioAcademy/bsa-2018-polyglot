using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Polyglot.DataAccess.Elasticsearch;
using Polyglot.DataAccess.ElasticsearchModels;

namespace Polyglot.DataAccess.Entities
{
    public class Project : Entity, ISearcheable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Technology { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual UserProfile UserProfile { get; set; }


		[ForeignKey("MainLanguageId")]
        public virtual Language MainLanguage { get; set; }

		public int? MainLanguageId { get; set; }

        public virtual ICollection<ComplexString> Translations { get; set; }
        public virtual ICollection<ProjectLanguage> ProjectLanguageses { get; set; }
        public virtual ICollection<ProjectGlossary> ProjectGlossaries { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<ProjectTeam> ProjectTeams { get; set; }


        public Project()
        {
            ProjectTeams = new List<ProjectTeam>();
            Translations = new List<ComplexString>();
            ProjectLanguageses = new List<ProjectLanguage>();
            ProjectGlossaries = new List<ProjectGlossary>();
        }

        public IIndexObject GetIndexObject()
        {
            return new ProjectIndex
            {
                Id = Id.ToString(),
                Name = Name
            };
        }
    }
}
