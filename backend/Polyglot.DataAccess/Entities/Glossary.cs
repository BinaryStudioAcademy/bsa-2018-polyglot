using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polyglot.DataAccess.Entities
{
    public class Glossary : Entity
    {
        
        public virtual ICollection<GlossaryString> GlossaryStrings { get; set; }

        public string Name { get; set; }

        [ForeignKey("OriginLanguageId")]
        public virtual Language OriginLanguage { get; set; }
        public int? OriginLanguageId { get; set; }

        public virtual ICollection<ProjectGlossary> ProjectGlossaries { get; set; }

        public Glossary()
        {
            GlossaryStrings = new List<GlossaryString>();
            ProjectGlossaries = new List<ProjectGlossary>();
        }
    }
}
