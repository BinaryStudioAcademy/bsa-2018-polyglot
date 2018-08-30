using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Polyglot.DataAccess.ElasticsearchModels;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Elasticsearch
{
    public static class ElasticsearchExtensions
    {

        private static IElasticClient _elasticClient;

        private static IElasticLowLevelClient _lowlevelClient;

        private static ConnectionSettings _settings;

        private static bool _updateSearchService;


        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];
            _updateSearchService = Boolean.Parse(configuration["elasticsearch:updateIndex"]);

            _settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<ComplexStringIndex>(m => m
                    .IndexName("complexstring")
                    .TypeName("complexstring")
                )
                .DefaultMappingFor<ProjectIndex>(m => m
                    .IndexName("project")
                    .TypeName("project")
                );

            _elasticClient = new ElasticClient(_settings);

            services.AddSingleton<IElasticClient>(_elasticClient);


            var settingslow = new ConnectionConfiguration(new Uri(url))
                .RequestTimeout(TimeSpan.FromMinutes(2));

            _lowlevelClient = new ElasticLowLevelClient(settingslow);
        }

        public static IElasticClient GetElasticClient<T>() where T : Entity, new()
        {
            return _elasticClient;
        }

        public static async Task UpdateSearchIndex<T>(T entityToUpdate, CrudAction action) where T : Entity, new()
        {
            if (_updateSearchService)
            {
                if (entityToUpdate is ISearcheable searchable)
                {
                    var indexObject = searchable.GetIndexObject();
                    if (indexObject != null)
                    {
                        var targetType = typeof(T).Name.ToLower();
                        try
                        {
                            StringResponse response;
                            switch (action)
                            {
                                case CrudAction.Create:
                                    {
                                        indexObject.CreatedAt = DateTime.Now;
                                        response = await _lowlevelClient.IndexAsync<StringResponse>(targetType, targetType,
                                            indexObject.Id, PostData.Serializable(indexObject));
                                    }
                                    break;
                                case CrudAction.Update:
                                    {
                                        indexObject.UpdatedAt = DateTime.Now;
                                        response =
                                        await _lowlevelClient.IndexPutAsync<StringResponse>(targetType, targetType,
                                            indexObject.Id, PostData.Serializable(indexObject));
                                        break;
                                    }
                                case CrudAction.Delete:
                                    {
                                        response =
                                            await _lowlevelClient.DeleteAsync<StringResponse>(targetType, targetType,
                                                indexObject.Id);
                                        break;
                                    }
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
            }
        }
    }
}
