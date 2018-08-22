using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public class ChartDTO
    {
        public string Name { get; set; }
        public Dictionary<IEnumerable<string>, int> Values { get; set; }
    }
}
