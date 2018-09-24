using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public  class FileMetaDTO
	{
		public string Project { get; set; }
		public string Description { get; set; }
		public string Owner { get; set; }
		public string Updated { get; set; }
		public List<string> SupportedLanguages { get; set; }
		public List<string> Translators { get; set; }
	}
}
