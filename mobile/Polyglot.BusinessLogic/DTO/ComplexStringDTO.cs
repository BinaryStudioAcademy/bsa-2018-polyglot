using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Polyglot.BusinessLogic.DTO
{
    public class ComplexStringDTO
    {
        public string Id { get; set; }

        public string Key { get; set; }

        [JsonProperty("base")]
        public string OriginalValue { get; set; }

        public string Description { get; set; }
    }
}
