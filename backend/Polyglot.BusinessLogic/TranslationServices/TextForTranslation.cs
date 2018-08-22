using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Polyglot.BusinessLogic.TranslationServices
{
    public class TextForTranslation
    {
        [Required]
        [JsonProperty(PropertyName = "q")]
        public string Text { get; set; }

        [Required]
        [JsonProperty(PropertyName = "target")]
        public string TargetLanguageCode { get; set; }

        [JsonProperty(PropertyName = "source")]
        public string SourceLanguageCode { get; set; }
    }
}
