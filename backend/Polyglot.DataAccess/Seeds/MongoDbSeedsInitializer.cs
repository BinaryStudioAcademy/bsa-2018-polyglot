
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Polyglot.DataAccess.MongoModels;
using Polyglot.DataAccess.MongoRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.Seeds
{
    public class MongoDbSeedsInitializer
    {

        public static void MongoSeedAsync(MongoDataContext context)
        {
            if (context.ComplexStrings.CountDocuments(FilterDefinition<ComplexString>.Empty) == 0)
            
            {
                var complexStrings = new List<ComplexString>
            {
                new ComplexString {
                    Id = 1,
                    Key="title",
                    ProjectId = 1,
                    Language="English",
                    OriginalValue = "Operation Valkyrie",
                    Description ="file title",
                    PictureLink="http://www.lib.cam.ac.uk/files/stauffenberg-bendler.jpg",
                    Translations = new List<Translation> {
                        new Translation { Language = "Ukranian", TranslationValue ="Операція Валькірія", CreatedOn = DateTime.Now, UserId = 1,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 4,
                                TranslationValue = "Операція Валкірі",
                                CreatedOn = DateTime.Now
                                }
                            },

                        OptionalTranslations = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 3,
                                TranslationValue = "Операція Валкірі",
                                CreatedOn = DateTime.Now
                                }
                            }
                        },
                        new Translation { Language = "German", TranslationValue ="Unternehmen Walküre", CreatedOn = DateTime.Now, UserId = 2,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 5,
                                TranslationValue = "Walküre Unternehmen",
                                CreatedOn = DateTime.Now
                                }
                            },

                        OptionalTranslations = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 3,
                                TranslationValue = "Unternehmen Walküre",
                                CreatedOn = DateTime.Now
                                }
                            }
                        }
                    },
                    Comments = new List<Comment> {
                        new Comment {
                            UserId = 4,
                            Text="I think it's a great job!",
                            CreatedOn= DateTime.Now
                        },
                         new Comment {
                            UserId = 2,
                            Text="Awsome!",
                            CreatedOn= DateTime.Now
                        }
                    },
                    Tags = new List<string> { "WW2", "title", "test" }

                },
                new ComplexString {
                    Id = 2,
                    Key="Differences between Angular and AngularJS",
                    ProjectId = 5,
                    Language="English",
                    OriginalValue = "Angular does not have a concept of 'scope' or controllers, instead it uses a hierarchy of components as its primary architectural characteristic",
                    Description ="file title",
                    PictureLink="http://www.lib.cam.ac.uk/files/stauffenberg-bendler.jpg",
                    Translations = new List<Translation> {
                        new Translation { 
                            Language = "Ukranian", 
                            TranslationValue ="Angular не має поняття області видимості або контроллера, замість цього він використовує ієрархію компонентів як основну архітектурну характеристику", 
                            CreatedOn = DateTime.Now, 
                            UserId = 1,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 4,
                                TranslationValue = "Angular не має поняття областей видимості або контроллерів, замість цього він використовує ієрархію компонентів як основну архітектурну характеристику",
                                CreatedOn = DateTime.Now
                                }
                            },

                        OptionalTranslations = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 3,
                                TranslationValue = "Angular не має поняття областей видимості або контроллерів, натомість як основну архітектурну характеристику він використовує ієрархію компонентів",
                                CreatedOn = DateTime.Now
                                }
                            }
                        },
                        new Translation { Language = "Italian", TranslationValue ="Unternehmen Walküre", CreatedOn = DateTime.Now, UserId = 2,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 5,
                                TranslationValue = "Angular non ha un concetto di scope o controller, ma utilizza una gerarchia di componenti come sua principale caratteristica architettonica.",
                                CreatedOn = DateTime.Now
                                }
                            },

                        OptionalTranslations = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 3,
                                TranslationValue = "Angular non ha un concetto di scope o controller, ma utilizza una gerarchia di componenti come sua principale caratteristica architettonica.",
                                CreatedOn = DateTime.Now
                                }
                            }
                        }
                    },
                    Comments = new List<Comment> {
                        new Comment {
                            UserId = 5,
                            Text="I think it's a great job!",
                            CreatedOn= DateTime.Now
                        },
                         new Comment {
                            UserId = 1,
                            Text="Awsome!",
                            CreatedOn= DateTime.Now
                        }
                    },
                    Tags = new List<string> { "angular", "scope", "component" }

                },

            };
                context.ComplexStrings.InsertMany(complexStrings);

            }

          


        }
    }
}
