using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.BusinessLogic.DTO
{
    public class LanguageStatisticDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int TranslatedStringsCount { get; set; }

        public int ComplexStringsCount { get; set; }
    }
}
