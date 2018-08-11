using System;
using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Project : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Technology { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }

        
        public Manager Manager { get; set; }
        
        public Language MainLanguage { get; set; }

        public List<Team> Teams { get; set; }
        public List<Translation> Translations { get; set; }
        public List<ProjectLanguage> ProjectLanguageses { get; set; }
        public List<ProjectGlossary> ProjectGlossaries { get; set; }
        public List<ProjectTag> ProjectTags { get; set; }

        public Project()
        {
            Teams = new List<Team>();
            Translations = new List<Translation>();
            ProjectLanguageses = new List<ProjectLanguage>();
            ProjectGlossaries = new List<ProjectGlossary>();
            ProjectTags = new List<ProjectTag>();
        }
    }
}
