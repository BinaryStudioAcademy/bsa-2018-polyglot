using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Language : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Glossary> Glossaries { get; set; }
    }
}
