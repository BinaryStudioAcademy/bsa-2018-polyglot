using System;
using System.Collections.Generic;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.MongoModels
{
    public class ComplexString : Entity
    {
		public string Key { get; set; }
		public int ProjectId { get; set; }
        public int LanguageId { get; set; }
        public string OriginalValue { get; set; }
        public string Description { get; set; }
        public string PictureLink { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        public List<Translation> Translations { get; set; }
        public List<Comment> Comments { get; set; }
        public List<int> Tags { get; set; }

        public ComplexString()
        {

        }
    }
}
