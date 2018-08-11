using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Seeds
{
    public static class LanguagesModelBuilder
    {
        public static void LanguageSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(
            new Language
            {
                Id = 1,
                Name = "English",
                Code = "en"

            },
            new Language
            {
                Id = 2,
                Name = "Ukrainian",
                Code = "uk"

            },
            new Language
            {
                Id = 3,
                Name = "German",
                Code = "de"

            },
            new Language
            {
                Id = 4,
                Name = "Spanish",
                Code = "es"

            },
            new Language
            {
                Id = 5,
                Name = "Italian",
                Code = "it"

            }

        );

           
        }
    }
}
