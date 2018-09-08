using System;
using System.Collections.Generic;
using Polyglot.DataAccess.Elasticsearch;
using Polyglot.DataAccess.MongoModels;

namespace Polyglot.DataAccess.ElasticsearchModels
{
    public class ComplexStringIndex : IIndexObject
    {
        public ComplexStringIndex()
        {
			Tags = new List<int>();
        }

        public string Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Key { get; set; }
        public int ProjectId { get; set; }
        public int LanguageId { get; set; }
        public string OriginalValue { get; set; }
        public string Description { get; set; }
        public string PictureLink { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Translation> Translations { get; set; }

		public List<int> Tags { get; set; }
		public int CreatedBy { get; set; }
	}
}
