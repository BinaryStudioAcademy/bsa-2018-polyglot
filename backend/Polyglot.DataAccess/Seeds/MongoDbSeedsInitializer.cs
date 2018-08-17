using Polyglot.DataAccess.MongoModels;
using Polyglot.DataAccess.MongoRepository;
using System;
using System.Collections.Generic;

namespace Polyglot.DataAccess.Seeds
{
    public class MongoDbSeedsInitializer
    {

        public static void MongoSeedAsync(IMongoDataContext context)
        {
            var repository = new MongoRepository<ComplexString>(context);
            if (repository.CountDocuments() == 0)
            
            {
                var complexStrings = new List<ComplexString>
            {
                new ComplexString {
                    Id = 1,
                    Key="title",
                    ProjectId = 3,
                    Language="English",
                    OriginalValue = "Operation Valkyrie",
                    Description ="file title",
                    PictureLink="http://www.lib.cam.ac.uk/files/stauffenberg-bendler.jpg",
                    Translations = new List<Translation> {
                        new Translation {
                        Id = Guid.NewGuid(),
                        LanguageId = 2, 
                        TranslationValue ="Операція Валькірія", 
                        CreatedOn = DateTime.Now, 
                        UserId = 1,
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
                        new Translation {
                            Id = Guid.NewGuid(),
                            LanguageId = 3, 
                            TranslationValue ="Unternehmen Walküre", 
                            CreatedOn = DateTime.Now, UserId = 2,
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
                       /* new Comment {
                            UserId = 4,
                            Text="I think it's a great job!",
                            CreatedOn= DateTime.Now
                        },
                         new Comment {
                            UserId = 2,
                            Text="Awsome!",
                            CreatedOn= DateTime.Now
                        }*/
                    },
                    Tags = new List<string> { "WW2", "title", "test" }

                },
                new ComplexString {
                    Id = 2,
                    Key="Differences between Angular and AngularJS",
                    ProjectId = 5,
                    Language="English",
                    OriginalValue = "Angular does not have a concept of 'scope' or controllers, instead it uses a hierarchy of components as its primary architectural characteristic",
                    Description ="Technology difference",
                    PictureLink="https://qph.fs.quoracdn.net/main-qimg-06a25d4b46bae22b0d583e6e4ee3472b",
                    Translations = new List<Translation> {
                        new Translation {
                            Id = Guid.NewGuid(),
                            LanguageId = 2, 
                            TranslationValue ="Angular не має поняття області видимості або контроллера, замість цього він використовує ієрархію компонентів як основну архітектурну характеристику", 
                            CreatedOn = DateTime.Now, 
                            UserId = 5,
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
                        new Translation {   Id = Guid.NewGuid(),LanguageId = 5, TranslationValue ="Unternehmen Walküre", CreatedOn = DateTime.Now, UserId = 2,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 2,
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
                      /*  new Comment {
                            UserId = 5,
                            Text="I think it's a great job!",
                            CreatedOn= DateTime.Now
                        },
                         new Comment {
                            UserId = 2,
                            Text="Awsome!",
                            CreatedOn= DateTime.Now
                        }*/
                    },
                    Tags = new List<string> { "angular", "scope", "component" }

                },
                new ComplexString {
                    Id = 3,
                    Key="Perspectives",
                    ProjectId = 4,
                    Language="English",
                    OriginalValue = "With .NET Core 3 the framework will get support for development of Desktop, Artificial Intelligence/Machine Learning and IoT apps.",
                    Description =".NET Core 3.0 announced",
                    PictureLink="https://pbs.twimg.com/media/DcsAmkxXcAIl_en.jpg",
                    Translations = new List<Translation> {
                        new Translation {
                            Id = Guid.NewGuid(),
                            LanguageId = 2, 
                            TranslationValue ="З виходом .NET Core 3 фреймворк отримає підтримку для розробки програм для робочого столу, штучного інтелекту / машинного навчання та інтернету речей.", 
                            CreatedOn = DateTime.Now, 
                            UserId = 1,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 4,
                                TranslationValue = "З релізом . NET Core 3 фреймворк отримає підтримку для розробки додатків для робочого столу, штучного інтелекту / машинного навчання та IoT.",
                                CreatedOn = DateTime.Now
                                }
                            },

                        OptionalTranslations = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 3,
                                TranslationValue = "За допомогою .NET Core 3 система отримає підтримку для розробки програм для робочого столу, штучного інтелекту / машинного навчання та IoT.",
                                CreatedOn = DateTime.Now
                                }
                            }
                        },
                        new Translation {
                            Id = Guid.NewGuid(),
                            LanguageId = 5, 
                            TranslationValue ="Avec Core 3, le framework prendra en charge le développement d'applications de bureau, d'intelligence artificielle / d'apprentissage automatique et d'IoT", 
                            CreatedOn = DateTime.Now, 
                            UserId = 2,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 4,
                                TranslationValue = "Avec Core 3, le framework prendra en charge le développement d'applications de bureau, d'intelligence artificielle / d'apprentissage automatique et d'IoT.",
                                CreatedOn = DateTime.Now
                                }
                            },

                        OptionalTranslations = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 3,
                                TranslationValue = "Avec Core 3, le framework prendra en charge le développement d'applications de bureau, d'intelligence artificielle / d'apprentissage automatique et d'IoT.",
                                CreatedOn = DateTime.Now
                                }
                            }
                        }
                    },
                    Comments = new List<Comment> {
                       /* new Comment {
                            UserId = 5,
                            Text="I think it's a great job!",
                            CreatedOn= DateTime.Now
                        },
                         new Comment {
                            UserId = 2,
                            Text="Awsome!",
                            CreatedOn= DateTime.Now
                        }*/
                    },
                    Tags = new List<string> { "net core", "announce", "framework" }

                },
                new ComplexString {
                    Id = 4,
                    Key="Production",
                    ProjectId = 1,
                    Language="English",
                    OriginalValue = "Principal photography began in mid February 2017 in Morocco and employed 400 Moroccans and 300 Chinese as part of the technical crew group",
                    Description ="Features of the release of the film",
                    PictureLink="https://ichef.bbci.co.uk/news/660/media/images/82107000/jpg/_82107031_chinesenavyreut.jpg",
                    Translations = new List<Translation> {
                        new Translation {
                            Id = Guid.NewGuid(),
                            LanguageId = 5, 
                            TranslationValue ="主要攝影於2017年2月中旬在摩洛哥開始，僱用了400名摩洛哥人和300名中國人作為技術人員組的一部分", 
                            CreatedOn = DateTime.Now, 
                            UserId = 1,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 1,
                                TranslationValue = "主要攝影於2017年2月中旬在摩洛哥開始，僱用了400名摩洛哥人和300名中國人作為技術人員組的一部分",
                                CreatedOn = DateTime.Now
                                }
                            },

                        OptionalTranslations = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 3,
                                TranslationValue = "主要攝影於2017年2月中旬在摩洛哥開始，僱用了400名摩洛哥人和300名中國人作為技術人員組的一部分",
                                CreatedOn = DateTime.Now
                                }
                            }
                        },
                        new Translation {
                            Id = Guid.NewGuid(),
                            LanguageId = 4, 
                            TranslationValue ="La fotografía principal comenzó a mediados de febrero de 2017 en Marruecos y empleó a 400 marroquíes y 300 chinos como parte del grupo técnico de tripulación.", 
                            CreatedOn = DateTime.Now, 
                            UserId = 2,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 5,
                                TranslationValue = "La fotografía principal comenzó a mediados de febrero de 2017 en Marruecos y empleó a 400 marroquíes y 300 chinos como parte del grupo técnico de tripulación.",
                                CreatedOn = DateTime.Now
                                }
                            },

                        OptionalTranslations = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 3,
                                TranslationValue = "La fotografía principal comenzó a mediados de febrero de 2017 en Marruecos y empleó a 400 marroquíes y 300 chinos como parte del grupo técnico de tripulación.",
                                CreatedOn = DateTime.Now
                                }
                            }
                        }
                    },
                    Comments = new List<Comment> {
                      /*  new Comment {
                            UserId = 3,
                            Text="I think it's a great job!",
                            CreatedOn= DateTime.Now
                        },
                         new Comment {
                            UserId = 2,
                            Text="Awsome!",
                            CreatedOn= DateTime.Now
                        }*/
                    },
                    Tags = new List<string> { "crew", "film", "marocco" }

                },
                new ComplexString {
                    Id = 5,
                    Key="article",
                    ProjectId = 2,
                    Language="English",
                    OriginalValue = "The geopolitical disposition of Europe in 1941, immediately before the start of Operation Barbarossa. The grey area represents Nazi Germany, its allies, and countries under its firm control " +
                                    "In August 1939, Germany and the Soviet Union signed a non-aggression pact in Moscow known as the Molotov–Ribbentrop Pact",
                    Description ="German-Soviet relations of 1939–40",
                    PictureLink="https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Tweede_wereldoorlog_inval_in_Polen_1939.png/800px-Tweede_wereldoorlog_inval_in_Polen_1939.png",
                    Translations = new List<Translation> {
                        new Translation {
                            Id = Guid.NewGuid(),
                            LanguageId = 3, 
                            TranslationValue ="Die geopolitische Disposition Europas 1941, unmittelbar vor Beginn der Operation Barbarossa. Die Grauzone repräsentiert Nazi-Deutschland, seine Verbündeten und die von ihm kontrollierten Länder. " +
                                            "Im August 1939 unterzeichneten Deutschland und die Sowjetunion in Moskau einen Nichtangriffspakt, der Molotov-Ribbentrop-Pakt", 
                            CreatedOn = DateTime.Now, 
                            UserId = 1,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 1,
                                TranslationValue = "Die geopolitische Disposition Europas 1941, unmittelbar vor Beginn der Operation Barbarossa. Die Grauzone repräsentiert Nazi-Deutschland, seine Verbündeten und die von ihm kontrollierten Länder. " +
                                                    "Im August 1939 unterzeichneten Deutschland und die Sowjetunion in Moskau einen Nichtangriffspakt, der Molotov-Ribbentrop-Pakt",
                                CreatedOn = DateTime.Now
                                }
                            },

                        OptionalTranslations = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 3,
                                TranslationValue = "Die geopolitische Disposition Europas 1941, unmittelbar vor Beginn der Operation Barbarossa. Die Grauzone repräsentiert Nazi-Deutschland, seine Verbündeten und die von ihm kontrollierten Länder. " +
                                                    "Im August 1939 unterzeichneten Deutschland und die Sowjetunion in Moskau einen Nichtangriffspakt, der Molotov-Ribbentrop-Pakt",
                                CreatedOn = DateTime.Now
                                }
                            }
                        },
                        new Translation {
                            Id = Guid.NewGuid(),
                            LanguageId = 2, 
                            TranslationValue ="Геополітичне розташування Європи в 1941 році, безпосередньо перед початком операції Барбаросса. Сірий район представляє нацистську Німеччину, її союзників та країни, що знаходяться під його жорстким контролем. " +
                                                "У серпні 1939 р. Німеччина та Радянський Союз підписали пакт про ненапад, який в Москві називають пакт Молотова-Ріббентропа", 
                            CreatedOn = DateTime.Now, 
                            UserId = 3,
                        History = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 1,
                                TranslationValue ="Геополітичне розташування Європи в 1941 році, безпосередньо перед початком операції Барбаросса. Сірий район представляє нацистську Німеччину, її союзників та країни, що знаходяться під його жорстким контролем. " +
                                                "У серпні 1939 р. Німеччина та Радянський Союз підписали пакт про ненапад, який в Москві називають пакт Молотова-Ріббентропа", 
                                CreatedOn = DateTime.Now, 
                                }
                            },

                        OptionalTranslations = new List<AdditionalTranslation> {
                            new AdditionalTranslation {
                                UserId = 2,
                                 TranslationValue ="Геополітичне розташування Європи в 1941 році, безпосередньо перед початком операції Барбаросса. Сірий район представляє нацистську Німеччину, її союзників та країни, що знаходяться під його жорстким контролем. " +
                                                "У серпні 1939 р. Німеччина та Радянський Союз підписали пакт про ненапад, який в Москві називають пакт Молотова-Ріббентропа", 
                                CreatedOn = DateTime.Now, 
                                }
                            }
                        }
                    },
                    Comments = new List<Comment> {
                       /* new Comment {
                            UserId = 1,
                            Text="I think it's a great job!",
                            CreatedOn= DateTime.Now
                        },
                         new Comment {
                            UserId = 4,
                            Text="Awsome!",
                            CreatedOn= DateTime.Now
                        }*/
                    },
                    Tags = new List<string> { "germany", "ussr", "ww2" }

                }

            };
                repository.InsertMany(complexStrings);

            }
        }
    }
}
