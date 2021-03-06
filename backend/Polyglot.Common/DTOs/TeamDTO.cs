﻿using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserProfileDTO CreatedBy { get; set; }
        
        public List<TranslatorDTO> TeamTranslators { get; set; }
        public List<TeamProjectDTO> TeamProjects { get; set; }

        public TeamDTO()
        {
            TeamTranslators = new List<TranslatorDTO>();
            TeamProjects = new List<TeamProjectDTO>();
        }    
    }
}
