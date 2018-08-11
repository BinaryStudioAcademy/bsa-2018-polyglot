using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Seeds
{
    public static class ProjectsModelBuilder
    {
        public static void ProjectSeed(this ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Manager>(p => 
            { 
                p.HasData(new { Id = 1});
                
                p.OwnsOne(e=>e.Projects).HasData(
                
                    new List <Project>()  
                {
                    
                });
                
                });
                
            
           
        

        }
    }
}



//new Project
//            {
//                Id = 2,
//                Name = "Operation Red Sea",
//                Description = "Operation Red Sea (Chinese: 红海行动) is a 2018 Chinese action war film directed by Dante Lam and starring Zhang Yi, Huang Jingyu, Hai Qing, Du Jiang and Prince Mak. The film is loosely based on the evacuation of the 225 foreign nationals and almost 600 Chinese citizens from Yemen's southern port of Aden during late March in the 2015 Civil War.",
//                CreatedOn = DateTime.Now,
//                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/6/61/Operation_Red_Sea_poster.jpg"
//            },
//            new Project
//            {
//                Id = 3,
//                Name = "Operation Barbarossa",
//                Description = "Operation Barbarossa (German: Unternehmen Barbarossa) was the code name for the Axis invasion of the Soviet Union, which started on Sunday, 22 June 1941, during World War II.",
//                CreatedOn = DateTime.Now,
//                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/5f/Operation_Barbarossa_Infobox.jpg"
//            },
//            new Project
//            {
//                Id = 4,
//                Name = "Operation Finale",
//                Description = "Operation Finale is an upcoming American historical drama film directed by Chris Weitz and written by Matthew Orton.",
//                CreatedOn = DateTime.Now,
//                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/7/75/Operation_Finale.png"
//            },
//            new Project
//            {
//                Id = 5,
//                Name = "Angular",
//                Description = "Angular (commonly referred to as Angular 2 +  or Angular v2 and above) is a TypeScript-based open-source front-end web application platform led by the Angular Team at Google and by a community of individuals and corporations.",
//                CreatedOn = DateTime.Now,
//                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Angular_full_color_logo.svg/512px-Angular_full_color_logo.svg.png"
//            }