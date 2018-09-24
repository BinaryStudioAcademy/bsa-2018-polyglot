using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public class Local
    {
		public Dictionary<string,string> Records { get; set; }

		public Local()
		{
			Records = new Dictionary<string, string>();
		}
    }
}
