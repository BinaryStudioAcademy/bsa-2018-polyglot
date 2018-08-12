using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Glossary : Entity
    {
        public string TermText { get; set; }
        public string ExplanationText { get; set; }
        public string OriginLanguage { get; set; }
        
        public virtual ICollection<ProjectGlossary> ProjectGlossaries { get; set; }

        public Glossary()
        {
            ProjectGlossaries = new List<ProjectGlossary>();
        }
    }
}
