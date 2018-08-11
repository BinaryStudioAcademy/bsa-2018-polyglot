using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Seeds
{
    public static class TagsModelBuilder
    {
        public static void TagSeed(this ModelBuilder modelBuilder)
        {
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
            
        }
    }
}
