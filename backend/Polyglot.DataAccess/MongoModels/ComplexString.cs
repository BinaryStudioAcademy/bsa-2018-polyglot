using System.Collections.Generic;
using Nest;
using Polyglot.DataAccess.Elasticsearch;
using Polyglot.DataAccess.ElasticsearchModels;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.MongoModels
{
    [ElasticsearchType(Name = "attributed_project")]
    public class ComplexString : Entity, ISearcheable<ComplexString>
    {
		public string Key { get; set; }
		public int ProjectId { get; set; }
        public int LanguageId { get; set; }
        public string OriginalValue { get; set; }
        public string Description { get; set; }
        public string PictureLink { get; set; }

        public List<Translation> Translations { get; set; }
        public List<Comment> Comments { get; set; }
        public List<string> Tags { get; set; }

        public ComplexString()
        {

        }

        public ComplexString GetIndexObject()
        {
            return new ComplexString
            {
                Id = Id,
                LanguageId = LanguageId
            };
        }
    }
}
