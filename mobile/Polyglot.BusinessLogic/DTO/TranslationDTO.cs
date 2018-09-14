using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.BusinessLogic.DTO
{
    public class TranslationDTO
    {
        public Guid Id { get; set; }
        public int LanguageId { get; set; }
        public string TranslationValue { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
