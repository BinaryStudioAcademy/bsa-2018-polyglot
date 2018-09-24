using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public class Export
    {
		public FileMetaDTO MetaData { get; set; }
		public Dictionary<string, Dictionary<string, string>> Locales { get; set; }

		public Export()
		{
			Locales = new Dictionary<string, Dictionary<string, string>>();
		}
    }
}
