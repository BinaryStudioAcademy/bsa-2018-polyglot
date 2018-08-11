using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Seeds
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 1,
                Uid = "777",
                FullName = "John Lennon",
                BirthDate = DateTime.Parse("21.01.1996"),
                RegistrationDate = DateTime.Now,
                Country = "USA",
                City = "New York",
                Address = "570 Lexington Avenue",
                Region = "New York",
                AvatarUrl = "https://www.songhall.org/images/uploads/exhibits/John_Lennon.jpg",
                PostalCode = "10022",
                Phone = "1-800-746-4726"

            });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 2,
                Uid = "888",
                FullName = "Petro Mazepa",
                BirthDate = DateTime.Parse("21.01.1986"),
                RegistrationDate = DateTime.Now,
                Country = "Ukraine",
                City = "Kyiv",
                Address = "Holosiivskiy avenu, 100",
                Region = "Kyiv",
                AvatarUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/1/17/Batman-BenAffleck.jpg/200px-Batman-BenAffleck.jpg",
                PostalCode = "43022",
                Phone = "38-095-746-4726"

            });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 3,
                Uid = "88546",
                FullName = "Vasya Mykolaiychuk",
                BirthDate = DateTime.Parse("21.01.1999"),
                RegistrationDate = DateTime.Now,
                Country = "Ukraine",
                City = "Striy",
                Address = "Mazepy avenue, 67",
                Region = "Lviv",
                AvatarUrl = "https://pbs.twimg.com/profile_images/934857621709950977/VYahTdwt_400x400.jpg",
                PostalCode = "43022",
                Phone = "38-095-746-4726"

            });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 4,
                Uid = "34324",
                FullName = "Johan Nesbartl",
                BirthDate = DateTime.Parse("21.01.1993"),
                RegistrationDate = DateTime.Now,
                Country = "USA",
                City = "Maiami",
                Address = "3275 NW 24th Street Rd",
                Region = "Florida",
                AvatarUrl = "https://www.famousbirthdays.com/faces/bartl-johannes-image.jpg",
                PostalCode = "63022",
                Phone = "12-795-746-4726"

            });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 5,
                Uid = "34324",
                FullName = "Lele Pots",
                BirthDate = DateTime.Parse("21.01.1993"),
                RegistrationDate = DateTime.Now,
                Country = "USA",
                City = "Maiami",
                Address = "3275 NW 24th Street Rd",
                Region = "Florida",
                AvatarUrl = "https://i.telegraph.co.uk/multimedia/archive/03403/lelepons2_3403661k.jpg",
                PostalCode = "63022",
                Phone = "12-795-746-4726"

            });

            modelBuilder.Entity<Manager>().HasData(new { Id = 1, UserProfileId = 1 });
            modelBuilder.Entity<Manager>().HasData(new { Id = 2, UserProfileId = 1 });
            modelBuilder.Entity<Manager>().HasData(new { Id = 3, UserProfileId = 2 });
            modelBuilder.Entity<Manager>().HasData(new { Id = 4, UserProfileId = 3 });
            modelBuilder.Entity<Manager>().HasData(new { Id = 5, UserProfileId = 5 });


            modelBuilder.Entity<Translator>().HasData(new { Id = 1, UserProfileId = 1 });
            modelBuilder.Entity<Translator>().HasData(new { Id = 2, UserProfileId = 1 });
            modelBuilder.Entity<Translator>().HasData(new { Id = 3, UserProfileId = 2 });
            modelBuilder.Entity<Translator>().HasData(new { Id = 4, UserProfileId = 3 });
            modelBuilder.Entity<Translator>().HasData(new { Id = 5, UserProfileId = 5 });


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
                new Language { Id = 1, Name = "English", Code = "en"},
                new Language { Id = 2, Name = "Ukrainian", Code = "uk"},
                new Language { Id = 3, Name = "German", Code = "de"},
                new Language { Id = 4, Name = "Spanish", Code = "es"},
                new Language { Id = 5, Name = "Italian", Code = "it"}
                );


            modelBuilder.Entity<Project>().HasData(
                new
                {
                    Id = 1,
                    ManagerId = 1,
                    Name = "Operation Red Sea",
                    Description = "Operation Red Sea (Chinese: 红海行动) is a 2018 Chinese action war film directed by Dante Lam and starring Zhang Yi, Huang Jingyu, Hai Qing, Du Jiang and Prince Mak. The film is loosely based on the evacuation of the 225 foreign nationals and almost 600 Chinese citizens from Yemen's southern port of Aden during late March in the 2015 Civil War.",
                    CreatedOn = DateTime.Now,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/6/61/Operation_Red_Sea_poster.jpg",
                    MainLanguageId = 1
                },
                 new
                 {
                     Id = 2,
                     ManagerId = 1,
                     Name = "Operation Barbarossa",
                     Description = "Operation Barbarossa (German: Unternehmen Barbarossa) was the code name for the Axis invasion of the Soviet Union, which started on Sunday, 22 June 1941, during World War II.",
                     CreatedOn = DateTime.Now,
                     ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/5f/Operation_Barbarossa_Infobox.jpg",
                     MainLanguageId = 1
                 },
            new
            {
                Id = 3,
                ManagerId = 2,
                Name = "Operation Finale",
                Description = "Operation Finale is an upcoming American historical drama film directed by Chris Weitz and written by Matthew Orton.",
                CreatedOn = DateTime.Now,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/7/75/Operation_Finale.png",
                MainLanguageId = 1
            },
            new
            {
                Id = 4,
                ManagerId = 4,
                Name = "Angular",
                Description = "Angular (commonly referred to as Angular 2 +  or Angular v2 and above) is a TypeScript-based open-source front-end web application platform led by the Angular Team at Google and by a community of individuals and corporations.",
                CreatedOn = DateTime.Now,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Angular_full_color_logo.svg/512px-Angular_full_color_logo.svg.png",
                MainLanguageId = 1
            },
             new
             {
                 Id = 5,
                 ManagerId = 4,
                 Name = "ASP.NET Core",
                 Description = "ASP.NET Core is a free and open-source web framework, and higher performance than ASP.NET, developed by Microsoft and the community.",
                 CreatedOn = DateTime.Now,
                 ImageUrl = "https://ardalis.com/wp-content/uploads/2017/05/aspnetcore-logo-591x360.png",
                 MainLanguageId = 1
             }
                );

            modelBuilder.Entity<ProjectTag>().HasData(
                new { Id = 1, ProjectId = 1, TagId = 1 },
                new { Id = 2, ProjectId = 1, TagId = 2 },
                new { Id = 3, ProjectId = 2, TagId = 2 },
                new { Id = 4, ProjectId = 2, TagId = 4 },
                new { Id = 5, ProjectId = 3, TagId = 5 },
                new { Id = 6, ProjectId = 3, TagId = 6 },
                new { Id = 7, ProjectId = 4, TagId = 3 },
                new { Id = 8, ProjectId = 4, TagId = 4 },
                new { Id = 9, ProjectId = 5, TagId = 7 },
                new { Id = 10, ProjectId = 5, TagId = 8 }
                );



            modelBuilder.Entity<ProjectLanguage>().HasData(
              new { Id = 1, ProjectId = 1, LanguageId = 1 },
              new { Id = 2, ProjectId = 1, LanguageId = 2 },
              new { Id = 3, ProjectId = 2, LanguageId = 1 },
              new { Id = 4, ProjectId = 2, LanguageId = 3 },
              new { Id = 5, ProjectId = 3, LanguageId = 1 },
              new { Id = 6, ProjectId = 3, LanguageId = 4 },
              new { Id = 7, ProjectId = 4, LanguageId = 1 },
              new { Id = 8, ProjectId = 4, LanguageId = 5 },
              new { Id = 9, ProjectId = 5, LanguageId = 1 },
              new { Id = 10, ProjectId = 5, LanguageId = 3 }
              );







        }
    }
}

