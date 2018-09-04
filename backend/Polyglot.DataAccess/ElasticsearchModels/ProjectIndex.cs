using System;
using Polyglot.DataAccess.Elasticsearch;

namespace Polyglot.DataAccess.ElasticsearchModels
{
    public class ProjectIndex : IIndexObject
    {
        public string Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
    }
}