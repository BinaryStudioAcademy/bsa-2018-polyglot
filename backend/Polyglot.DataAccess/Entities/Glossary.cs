using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Glossary
    {
        public int Id { get; set; }
        public string TermText { get; set; }
        public string ExplanationText { get; set; }
        public string OriginLanguage { get; set; }
        
        public List<ProjectGlossary> ProjectGlossaries { get; set; }

        public Glossary()
        {
            ProjectGlossaries = new List<ProjectGlossary>();
        }
    }
}
