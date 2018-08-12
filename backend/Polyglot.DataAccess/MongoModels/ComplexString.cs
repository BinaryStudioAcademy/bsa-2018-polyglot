using System.Collections.Generic;

namespace Polyglot.DataAccess.MongoModels
{
    public class ComplexString : IEntity
    {
		public string Key { get; set; }
		public int ProjectId { get; set; }
        public string Language { get; set; }
        public string OriginalValue { get; set; }
        public string Description { get; set; }
        public string PictureLink { get; set; }

        public List<Translation> Translations { get; set; }
        public List<Comment> Comments { get; set; }
        public List<string> Tags { get; set; }

        public ComplexString()
        {

        }
    }
}
