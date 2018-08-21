using Newtonsoft.Json;

namespace Polyglot.BusinessLogic.TranslationServices
{
    public class TextForTranslation
    {
        [JsonProperty(PropertyName = "q")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "target")]
        public string TargetLanguageCode { get; set; }

        [JsonProperty(PropertyName = "source")]
        public string SourceLanguageCode { get; set; }
    }
}
