using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public class ProjectStatisticDTO
    {
        public IEnumerable<ChartDTO> Charts { get; set; }

        public ProjectStatisticDTO()
        {
            Charts = new List<ChartDTO>();
        }
    }
}
