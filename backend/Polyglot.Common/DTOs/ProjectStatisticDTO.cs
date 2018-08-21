using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public class ProjectStatisticDTO
    {
        public int Id { get; set; }

        public IEnumerable<LanguageDTO> Languages { get; set; }

        public IEnumerable<TranslatorDTO> Translators { get; set;}

        public ProjectStatisticDTO()
        {
            Languages = new List<LanguageDTO>();
            Translators = new List<TranslatorDTO>();
        }
    }
}
