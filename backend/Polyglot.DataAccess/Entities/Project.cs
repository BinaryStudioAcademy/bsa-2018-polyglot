using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polyglot.DataAccess.Entities
{
    public class Project : Entity
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

        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<ComplexString> Translations { get; set; }
        public virtual ICollection<ProjectLanguage> ProjectLanguageses { get; set; }
        public virtual ICollection<ProjectGlossary> ProjectGlossaries { get; set; }
        public virtual ICollection<ProjectTag> ProjectTags { get; set; }

        public Project()
        {
            Teams = new List<Team>();
            Translations = new List<ComplexString>();
            ProjectLanguageses = new List<ProjectLanguage>();
            ProjectGlossaries = new List<ProjectGlossary>();
            ProjectTags = new List<ProjectTag>();
        }
    }
}
