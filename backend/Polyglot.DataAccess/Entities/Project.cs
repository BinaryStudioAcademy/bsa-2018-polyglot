using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public List<Team> Teams { get; set; }
    }
}
