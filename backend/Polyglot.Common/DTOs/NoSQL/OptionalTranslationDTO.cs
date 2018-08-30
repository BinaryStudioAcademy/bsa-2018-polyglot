using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs.NoSQL
{
	public class OptionalTranslationDTO
	{
		public string UserPictureURL { get; set; }
		public string UserName { get; set; }
		public DateTime DateTime { get; set; }
		public string TranslationValue { get; set; }
	}
}
