using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Polyglot.BusinessLogic.DTO
{
    public class ComplexStringDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("projectId")]
        public int ProjectId { get; set; }
        [JsonProperty("base")]
        public string OriginalValue { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
