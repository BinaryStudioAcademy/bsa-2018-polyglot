using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Polyglot.Common.DTOs
{
    public class ChartDTO
    {
        public string Name { get; set; }
        public List<Point> Values { get; set; }
    }

    public class Point
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
