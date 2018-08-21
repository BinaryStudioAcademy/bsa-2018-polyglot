using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Glossary : Entity
    {
        
        public virtual ICollection<GlossaryString> GlossaryStrings { get; set; }

        public string OriginLanguage { get; set; }
        
        public virtual ICollection<ProjectGlossary> ProjectGlossaries { get; set; }

        public Glossary()
        {
            GlossaryStrings = new List<GlossaryString>();
            ProjectGlossaries = new List<ProjectGlossary>();
        }
    }
}
