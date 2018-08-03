using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class Translator
    {
        public int Id { get; set; }
        public UserProfile UserProfile { get; set; }
        public Rating Rating { get; set; }
    }
}
