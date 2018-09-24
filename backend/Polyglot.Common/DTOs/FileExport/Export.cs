using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Polyglot.Common.DTOs
{
    public class Export
    {
		[JsonProperty("metadata")]
		public FileMetaDTO MetaData { get; set; }

		[JsonProperty("locales")]
		public Dictionary<string, Dictionary<string, string>> Locales { get; set; }

		public Export()
		{
			Locales = new Dictionary<string, Dictionary<string, string>>();
		}
    }
}
