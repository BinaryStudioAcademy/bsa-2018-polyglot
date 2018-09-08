using System;
using System.Collections.Generic;
using Polyglot.DataAccess.Elasticsearch;
using Polyglot.DataAccess.ElasticsearchModels;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.MongoModels
{
    public class ComplexString : Entity, ISearcheable
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
			Tags = new List<int>();
        }

        public IIndexObject GetIndexObject()
        {
            return new ComplexStringIndex
            {
                Id = Id.ToString(),
                LanguageId = LanguageId,
                Translations = Translations,
                Comments = Comments,
                Description = Description,
                Key = Key,
                OriginalValue = OriginalValue,
                PictureLink = PictureLink,
                ProjectId = ProjectId,
				Tags = Tags,
				CreatedBy = CreatedBy
            };
        }
    }
}
