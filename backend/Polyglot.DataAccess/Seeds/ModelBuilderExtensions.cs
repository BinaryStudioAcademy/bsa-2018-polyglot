using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Helpers;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polyglot.DataAccess.Seeds
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().HasData(
                new UserProfile
                {
                    Id = 1,
                    Uid = "QqEx8KOLDZOYbKEiUxz18vkjKc22",
                    FullName = "Zhukov Mikhail",
                    BirthDate = new DateTime(1991, 9, 31),
                    RegistrationDate = DateTime.Now,
                    Country = "USA",
                    City = "New York",
                    Address = "570 Lexington Avenue",
                    Region = "New York",
                    AvatarUrl = "",
                    PostalCode = "10022",
                    Phone = "1-800-746-4726",
                    UserRole = Role.Manager

                });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 2,
                Uid = "Booik2DM64TX30VedRa0BY7AKZg1",
                FullName = "Petro Mazepa",
                BirthDate = new DateTime(1992, 9, 12),
                RegistrationDate = DateTime.Now,
                Country = "Ukraine",
                City = "Kyiv",
                Address = "Holosiivskiy avenu, 100",
                Region = "Kyiv",
                AvatarUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/1/17/Batman-BenAffleck.jpg/200px-Batman-BenAffleck.jpg",
                PostalCode = "43022",
                Phone = "38-095-746-4726",
                UserRole = Role.Manager
            });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 3,
                Uid = "5hdyMOGgPRZ3CREAYAGTAG815ZO2",
                FullName = "Vasya Mykolaiychuk",
                BirthDate = new DateTime(1991, 9, 25),
                RegistrationDate = DateTime.Now,
                Country = "Ukraine",
                City = "Striy",
                Address = "Mazepy avenue, 67",
                Region = "Lviv",
                AvatarUrl = "https://pbs.twimg.com/profile_images/934857621709950977/VYahTdwt_400x400.jpg",
                PostalCode = "43022",
                Phone = "38-095-746-4726",
                UserRole = Role.Manager
            });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 4,
                Uid = "bbgYGo9545Xcy84FUjmpHzYnESk2",
                FullName = "Johan Nesbartl",
                BirthDate = new DateTime(1991, 9, 25),
                RegistrationDate = DateTime.Now,
                Country = "USA",
                City = "Maiami",
                Address = "3275 NW 24th Street Rd",
                Region = "Florida",
                AvatarUrl = "https://www.famousbirthdays.com/faces/bartl-johannes-image.jpg",
                PostalCode = "63022",
                Phone = "12-795-746-4726",
                UserRole = Role.Manager

            });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 5,
                Uid = "HwMZdk6yMzUgK9wZsVIzzkCQURI2",
                FullName = "Lele Pots",
                BirthDate = new DateTime(1991, 9, 25),
                RegistrationDate = DateTime.Now,
                Country = "USA",
                City = "Maiami",
                Address = "3275 NW 24th Street Rd",
                Region = "Florida",
                AvatarUrl = "https://i.telegraph.co.uk/multimedia/archive/03403/lelepons2_3403661k.jpg",
                PostalCode = "63022",
                Phone = "12-795-746-4726",
                UserRole = Role.Manager

            });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 6,
                Uid = "bbgYGo9545Xcy84FUjmpHzYnESk2",
                FullName = "Translator1",
                BirthDate = new DateTime(1991, 9, 25),
                RegistrationDate = DateTime.Now,
                Country = "USA",
                City = "Maiami",
                Address = "3275 NW 24th Street Rd",
                Region = "Florida",
                AvatarUrl = "https://www.famousbirthdays.com/faces/bartl-johannes-image.jpg",
                PostalCode = "63022",
                Phone = "12-795-746-4726",
                UserRole = Role.Translator

            });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 7,
                Uid = "Translator2",
                FullName = "Lele Pots",
                BirthDate = new DateTime(1991, 9, 25),
                RegistrationDate = DateTime.Now,
                Country = "USA",
                City = "Maiami",
                Address = "3275 NW 24th Street Rd",
                Region = "Florida",
                AvatarUrl = "https://i.telegraph.co.uk/multimedia/archive/03403/lelepons2_3403661k.jpg",
                PostalCode = "63022",
                Phone = "12-795-746-4726",
                UserRole = Role.Translator


            });


            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Color = "Apple", Name = "csharp" },
                new Tag { Id = 2, Color = "Aqua", Name = "asp-net-core" },
                new Tag { Id = 3, Color = "Atomic tangerine", Name = "dotnet" },
                new Tag { Id = 4, Color = "Awesome", Name = "angular" },
                new Tag { Id = 5, Color = "Azure", Name = "binary-studio" },
                new Tag { Id = 6, Color = "Bittersweet", Name = "bsa18" },
                new Tag { Id = 7, Color = "Blue bell", Name = "firebase" },
                new Tag { Id = 8, Color = "Capri", Name = "www" },
                new Tag { Id = 9, Color = "Cameo pink", Name = "seeds" },
                new Tag { Id = 10, Color = "Blue-gray", Name = "mock" }
                );

            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Name = "English", Code = "en" },
                new Language { Id = 2, Name = "Ukrainian", Code = "uk" },
                new Language { Id = 3, Name = "German", Code = "de" },
                new Language { Id = 4, Name = "Spanish", Code = "es" },
                new Language { Id = 5, Name = "Italian", Code = "it" },
                new Language { Id = 6, Name = "Chinese", Code = "zh" },
                new Language { Id = 7, Name = "French", Code = "fr" },
                new Language { Id = 8, Name = "Japanese", Code = "ja" },
                new Language { Id = 9, Name = "Arabic", Code = "ar" },
                new Language { Id = 10, Name = "Czech", Code = "cs" },
                new Language { Id = 11, Name = "Esperanto", Code = "eo   " },
                new Language { Id = 12, Name = "Finnish", Code = "fi" },
                new Language { Id = 13, Name = "Georgian", Code = "ka" },
                new Language { Id = 14, Name = "Greek", Code = "el" },
                new Language { Id = 15, Name = "Hindi", Code = "hi" },
                new Language { Id = 16, Name = "Hungarian", Code = "hu" },
                new Language { Id = 17, Name = "Korean", Code = "ko" },
                new Language { Id = 18, Name = "Polish", Code = "pl" },
                new Language { Id = 19, Name = "Portuguese", Code = "pt" },
                new Language { Id = 20, Name = "Swedish", Code = "sv" },
                new Language { Id = 21, Name = "Turkish", Code = "tr" }
                );


            //modelBuilder.Entity<Project>().HasData(
            //    new
            //    {
            //        Id = 1,
            //        UserProfileId = 1,
            //        Name = "Operation Red Sea",
            //        Description = "Operation Red Sea (Chinese: 红海行动) is a 2018 Chinese action war film directed by Dante Lam and starring Zhang Yi, Huang Jingyu, Hai Qing, Du Jiang and Prince Mak. The film is loosely based on the evacuation of the 225 foreign nationals and almost 600 Chinese citizens from Yemen's southern port of Aden during late March in the 2015 Civil War.",
            //        CreatedOn = DateTime.Now,
            //        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/6/61/Operation_Red_Sea_poster.jpg",
            //        MainLanguageId = 1
            //    },
            //     new
            //     {
            //         Id = 2,
            //         UserProfileId = 2,
            //         Name = "Operation Barbarossa",
            //         Description = "Operation Barbarossa (German: Unternehmen Barbarossa) was the code name for the Axis invasion of the Soviet Union, which started on Sunday, 22 June 1941, during World War II.",
            //         CreatedOn = DateTime.Now,
            //         ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/5f/Operation_Barbarossa_Infobox.jpg",
            //         MainLanguageId = 1
            //     },
            //new
            //{
            //    Id = 3,
            //    UserProfileId = 3,
            //    Name = "Operation Valkyrie",
            //    Description = "Operation Valkyrie (German: Unternehmen Walküre) was a German World War II emergency continuity of government operations plan issued to the Territorial Reserve Army of Germany to execute and implement in case of a general breakdown in civil order of the nation.",
            //    CreatedOn = DateTime.Now,
            //    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/54/Claus_von_Stauffenberg_portrait_%281907-1944%29.JPG",
            //    MainLanguageId = 1
            //},
            //new
            //{
            //    Id = 4,
            //    UserProfileId = 4,
            //    Name = "Angular",
            //    Description = "Angular (commonly referred to as Angular 2 +  or Angular v2 and above) is a TypeScript-based open-source front-end web application platform led by the Angular Team at Google and by a community of individuals and corporations.",
            //    CreatedOn = DateTime.Now,
            //    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Angular_full_color_logo.svg/512px-Angular_full_color_logo.svg.png",
            //    MainLanguageId = 1
            //},
            // new
            // {
            //     Id = 5,
            //     UserProfileId = 5,
            //     Name = "ASP.NET Core",
            //     Description = "ASP.NET Core is a free and open-source web framework, and higher performance than ASP.NET, developed by Microsoft and the community.",
            //     CreatedOn = DateTime.Now,
            //     ImageUrl = "https://ardalis.com/wp-content/uploads/2017/05/aspnetcore-logo-591x360.png",
            //     MainLanguageId = 1
            // }
            //    );

         
            //modelBuilder.Entity<ProjectLanguage>().HasData(
            //  new { Id = 1, ProjectId = 1, LanguageId = 1 },
            //  new { Id = 2, ProjectId = 1, LanguageId = 2 },
            //  new { Id = 3, ProjectId = 2, LanguageId = 1 },
            //  new { Id = 4, ProjectId = 2, LanguageId = 3 },
            //  new { Id = 5, ProjectId = 3, LanguageId = 1 },
            //  new { Id = 6, ProjectId = 3, LanguageId = 4 },
            //  new { Id = 7, ProjectId = 4, LanguageId = 1 },
            //  new { Id = 8, ProjectId = 4, LanguageId = 5 },
            //  new { Id = 9, ProjectId = 5, LanguageId = 1 },
            //  new { Id = 10, ProjectId = 5, LanguageId = 3 }
            //  );

            modelBuilder.Entity<TranslatorLanguage>().HasData(
             new { Id = 1, TranslatorId = 1, LanguageId = 1, Proficiency = "Expert" },
             new { Id = 2, TranslatorId = 1, LanguageId = 2, Proficiency = "Medium" },
             new { Id = 3, TranslatorId = 2, LanguageId = 1, Proficiency = "Expert" },
             new { Id = 4, TranslatorId = 2, LanguageId = 3, Proficiency = "Beginner" },
             new { Id = 5, TranslatorId = 3, LanguageId = 1, Proficiency = "Medium" },
             new { Id = 6, TranslatorId = 3, LanguageId = 4, Proficiency = "Expert" },
             new { Id = 7, TranslatorId = 4, LanguageId = 1, Proficiency = "Medium" },
             new { Id = 8, TranslatorId = 4, LanguageId = 5, Proficiency = "Beginner" },
             new { Id = 9, TranslatorId = 5, LanguageId = 1, Proficiency = "Expert" },
             new { Id = 10, TranslatorId = 5, LanguageId = 3, Proficiency = "Medium" }
             );

            modelBuilder.Entity<Right>().HasData(
             new Right { Id = 1, Definition = RightDefinition.AddNewKey },
             new Right { Id = 2, Definition = RightDefinition.AddNewLanguage },
             new Right { Id = 3, Definition = RightDefinition.CanAcceptTranslations },
             new Right { Id = 4, Definition = RightDefinition.AddNewKey },
             new Right { Id = 5, Definition = RightDefinition.AddNewKey }
            );

            modelBuilder.Entity<Team>().HasData(
             new { Id = 1, ProjectId = 5 },
             new { Id = 2, ProjectId = 2 },
             new { Id = 3, ProjectId = 3 },
             new { Id = 4, ProjectId = 1 },
             new { Id = 5, ProjectId = 4 }
            );


            modelBuilder.Entity<TeamTranslator>().HasData(
            new { Id = 1, TeamId = 1, TranslatorId = 1 },
            new { Id = 2, TeamId = 1, TranslatorId = 2 },
            new { Id = 3, TeamId = 1, TranslatorId = 1 },
            new { Id = 4, TeamId = 1, TranslatorId = 3 },
            new { Id = 5, TeamId = 1, TranslatorId = 2 },
            new { Id = 6, TeamId = 1, TranslatorId = 4 },
            new { Id = 7, TeamId = 1, TranslatorId = 3 },
            new { Id = 8, TeamId = 1, TranslatorId = 2 },
            new { Id = 9, TeamId = 1, TranslatorId = 1 },
            new { Id = 10, TeamId = 1, TranslatorId = 5 }

           );

            modelBuilder.Entity<TranslatorRight>().HasData(
                        new { Id = 1, RightId = 1, TeamTranslatorId = 1 },
                        new { Id = 2, RightId = 2, TeamTranslatorId = 1 },
                        new { Id = 3, RightId = 3, TeamTranslatorId = 1 },
                        new { Id = 4, RightId = 1, TeamTranslatorId = 2 },
                        new { Id = 5, RightId = 2, TeamTranslatorId = 2 },
                        new { Id = 6, RightId = 4, TeamTranslatorId = 2 },
                        new { Id = 7, RightId = 2, TeamTranslatorId = 3 },
                        new { Id = 8, RightId = 3, TeamTranslatorId = 3 },
                        new { Id = 9, RightId = 4, TeamTranslatorId = 3 },
                        new { Id = 10, RightId = 5, TeamTranslatorId = 4 },
                        new { Id = 11, RightId = 2, TeamTranslatorId = 4 },
                        new { Id = 12, RightId = 1, TeamTranslatorId = 4 }

                       );

            modelBuilder.Entity<Rating>().HasData(
            new { Id = 1, Rate = 80.0, TranslatorId = 1, Comment = "good job!", CreatedById = 1, CreatedAt = DateTime.Now },
            new { Id = 2, Rate = 100.0, TranslatorId = 1, Comment = "awsome!", CreatedById = 1, CreatedAt = DateTime.Now },
            new { Id = 3, Rate = 50.0, TranslatorId = 2, Comment = "not bad", CreatedById = 2, CreatedAt = DateTime.Now },
            new { Id = 4, Rate = 80.0, TranslatorId = 2, Comment = "good job!", CreatedById = 2, CreatedAt = DateTime.Now },
            new { Id = 5, Rate = 100.0, TranslatorId = 3, Comment = "awsome!", CreatedById = 3, CreatedAt = DateTime.Now },
            new { Id = 6, Rate = 60.0, TranslatorId = 4, Comment = "not bad", CreatedById = 3, CreatedAt = DateTime.Now },
            new { Id = 7, Rate = 80.0, TranslatorId = 4, Comment = "good job!", CreatedById = 4, CreatedAt = DateTime.Now },
            new { Id = 8, Rate = 100.0, TranslatorId = 5, Comment = "awsome!", CreatedById = 5, CreatedAt = DateTime.Now },
            new { Id = 9, Rate = 50.0, TranslatorId = 5, Comment = "not bad", CreatedById = 5, CreatedAt = DateTime.Now },
            new { Id = 10, Rate = 90.0, TranslatorId = 1, Comment = "good job!", CreatedById = 3, CreatedAt = DateTime.Now }

           );

            // modelBuilder.Entity<Glossary>().HasData(
            // new Glossary { Id = 1, TermText = "ABC", ExplanationText = "American-British-Canadian talks in 1941", OriginLanguage = "English" },
            // new Glossary { Id = 2, TermText = "MAAF", ExplanationText = "Mediterranean Allied Air Force", OriginLanguage = "English" },
            // new Glossary { Id = 3, TermText = "HIWI", ExplanationText = "Hilfsfreiwillige - German Army volunteer forces usually made up of Soviet volunteers1", OriginLanguage = "English" },
            // new Glossary { Id = 4, TermText = "Knickebein", ExplanationText = "Crooked Leg - German navigational system using radio beams to guide bombers", OriginLanguage = "English" },
            // new Glossary { Id = 5, TermText = "Humint", ExplanationText = "Human Intelligence - Intelligence gathered by spies and informers (as opposed to signals intelligence or SIGINT)", OriginLanguage = "English" },
            // new Glossary { Id = 6, TermText = "Kutusov", ExplanationText = "Operational code name for the Soviet offensive against German forces in the Kursk Salient - July 1943", OriginLanguage = "English" },
            // new Glossary { Id = 7, TermText = "CLR", ExplanationText = "Common Language Runtime.", OriginLanguage = "English" },
            // new Glossary { Id = 8, TermText = "NGEN", ExplanationText = "Native (image) generation.", OriginLanguage = "English" },
            // new Glossary { Id = 9, TermText = "Bootstrap", ExplanationText = "A way to initialize and launch an app or system.", OriginLanguage = "English" },
            // new Glossary { Id = 10, TermText = "Dependency injection", ExplanationText = "A design pattern and mechanism for creating and delivering parts of an application (dependencies) to other parts of an application that require them.", OriginLanguage = "English" }

            //);

           // modelBuilder.Entity<ProjectGlossary>().HasData(
           //     new { Id = 1, ProjectId = 1, GlossaryId = 1 },
           //     new { Id = 2, ProjectId = 1, GlossaryId = 2 },
           //     new { Id = 3, ProjectId = 2, GlossaryId = 3 },
           //     new { Id = 4, ProjectId = 2, GlossaryId = 4 },
           //     new { Id = 5, ProjectId = 3, GlossaryId = 5 },
           //     new { Id = 6, ProjectId = 3, GlossaryId = 6 },
           //     new { Id = 7, ProjectId = 4, GlossaryId = 7 },
           //     new { Id = 8, ProjectId = 4, GlossaryId = 8 },
           //     new { Id = 9, ProjectId = 5, GlossaryId = 9 },
           //     new { Id = 10, ProjectId = 5, GlossaryId = 10 }


           //);


            //modelBuilder.Entity<ComplexString>().HasData(
            //   new { Id = 1, ProjectId = 3, TranslationKey = "title" },
            //   new { Id = 2, ProjectId = 5, TranslationKey = "Differences between Angular and AngularJS" },
            //   new { Id = 3, ProjectId = 4, TranslationKey = "Perspectives" },
            //   new { Id = 4, ProjectId = 1, TranslationKey = "Production" },
            //   new { Id = 5, ProjectId = 2, TranslationKey = "article" }

            //   );


        }

        public static void EnsureSeeded(this DataContext context)
        {
            if (!context.Languages.Any())
            {
                var langs = new List<Language> {
                   new Language {  Name = "English", Code = "en" },
                   new Language {  Name = "Ukrainian", Code = "uk" },
                   new Language {  Name = "German", Code = "de" },
                   new Language {  Name = "Spanish", Code = "es" },
                   new Language {  Name = "Italian", Code = "it" },
				   new Language {  Name = "Esperanto", Code = "eo" },
				   new Language {  Name = "French", Code = "fr" },
				   new Language {  Name = "Chinese", Code = "zh" }
				   };
                context.AddRange(langs);
                context.SaveChanges();
            }

            if (!context.Rights.Any())
            {
                var rights = new List<Right> {
                    new Right { Definition = RightDefinition.AddNewKey },
                    new Right { Definition = RightDefinition.AddNewLanguage },
                    new Right { Definition = RightDefinition.CanAcceptTranslations },
                    
                };
                context.AddRange(rights);
                context.SaveChanges();
            }
        }
    }
}
