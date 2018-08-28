using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Polyglot.DataAccess.ElasticsearchModels;
using Polyglot.DataAccess.Entities;
using ComplexString = Polyglot.DataAccess.MongoModels.ComplexString;

namespace Polyglot.DataAccess.Elasticsearch
{
    public static class ElasticsearchExtensions
    {
        private static Dictionary<Type, IElasticClient> elasticClients = new Dictionary<Type, IElasticClient>();

        private static IElasticClient elasticClient;

        private static ConnectionSettings settings;

        private static bool _updateSearchService;


        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];
            _updateSearchService = Boolean.Parse(configuration["elasticsearch:updateIndex"]);

            settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<ComplexString>(m => m
                    .IndexName("complex-string")
                    .TypeName("complex-string")
                    //.RelationName("ComplexString")
                )
                .DefaultMappingFor<Project>(m => m
                    .IndexName("complex-string")
                    .TypeName("project")
                    //.RelationName("Project")
                );
            //.DefaultMappingFor<Post>(m => m
            //    .Ignore(p => p.IsPublished)
            //    .PropertyName(p => p.ID, "id")
            //)
            //.DefaultMappingFor<Comment>(m => m
            //    .Ignore(c => c.Email)
            //    .Ignore(c => c.IsAdmin)
            //    .PropertyName(c => c.ID, "id")
            //);
            //var resolver = new TypeNameResolver(settings);
            //var type = resolver.Resolve<ComplexString>();
            //type.s().Be("attributed_project");

            elasticClient = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(elasticClient);
        }

        //public static IElasticClient GetElasticClient()
        //{
        //    return elasticClient;
        //}

        public static IElasticClient GetElasticClient<T>()
            where T : DbEntity, new()
        {
            var targetType = typeof(T);
            if (elasticClients.ContainsKey(targetType))
            {
                return elasticClients[targetType];
            }
            else
            {
                //var newSettings = settings.DefaultIndex(targetType.Name)
                //    .DefaultMappingFor<ComplexStringIndex>(m => m
                //    .IndexName("ComplexString")
                //    .TypeName("ComplexString")
                //);
                var repoInstance = new ElasticClient(settings);
                elasticClients.Add(targetType, repoInstance);
                return repoInstance;
            }
            //return elasticClient;
        }

        public static async Task UpdateSearchIndex<T>(T entityToUpdate, CrudAction action) where T : DbEntity, new()
        {
            if (_updateSearchService)
            {
                if (entityToUpdate is ISearcheable<T> searchable)
                {
                    var indexObject = searchable.GetIndexObject();
                    if (indexObject != null)
                    {
                        //var elasticClient = GetElasticClient<T>();
                        var targetType = typeof(T);
                        switch (action)
                        {
                            case CrudAction.Create:
                                //var result = await elasticClient.IndexDocumentAsync(indexObject);
                                await elasticClient.IndexAsync(indexObject, idx => idx.Index(targetType.Name.ToLower()));
                                break;
                            case CrudAction.Update:
                            {
                                //var updateRequest = new UpdateRequest<IIndexObject, IIndexObject>(indexName, "people", docId)
                                //var path = new DocumentPath<IIndexObject>(indexObject
                                var result1 = await elasticClient.UpdateAsync<T>(new DocumentPath<T>(indexObject), e =>e.Doc(indexObject));
                                break;
                            }
                            case CrudAction.Delete:
                            {
                                await elasticClient.DeleteAsync<T>(new DocumentPath<T>(indexObject));
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    public enum CrudAction
    {
        Create,
        Update,
        Delete
    }
}
