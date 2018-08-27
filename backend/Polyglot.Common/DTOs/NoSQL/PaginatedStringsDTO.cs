using System.Collections.Generic;

namespace Polyglot.Common.DTOs.NoSQL
{
    public class PaginatedStringsDTO
    {
        public int TotalPages { get; set; }

        public IEnumerable<ComplexStringDTO> ComplexStrings { get; set; }
    }
}