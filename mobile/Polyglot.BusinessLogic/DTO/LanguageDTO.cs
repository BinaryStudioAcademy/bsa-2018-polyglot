using Newtonsoft.Json;

namespace Polyglot.BusinessLogic.Services
{
    public class LanguageDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
