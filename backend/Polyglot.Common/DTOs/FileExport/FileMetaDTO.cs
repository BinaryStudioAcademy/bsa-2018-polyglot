using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Polyglot.Common.DTOs
{
    public  class FileMetaDTO
	{
		[JsonProperty("project")]
		public string Project { get; set; }

		[JsonProperty("desription")]
		public string Description { get; set; }

		[JsonProperty("owner")]
		public string Owner { get; set; }

		[JsonProperty("updated")]
		public string Updated { get; set; }

		[JsonProperty("supported_languages")]
		public List<string> SupportedLanguages { get; set; }

		[JsonProperty("tranlsators")]
		public List<string> Translators { get; set; }

		[JsonProperty("source")]
		public string Source { get; set; }
	}
}
