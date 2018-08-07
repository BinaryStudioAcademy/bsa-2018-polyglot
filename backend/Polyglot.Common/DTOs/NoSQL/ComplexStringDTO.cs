using System.Collections.Generic;

namespace Polyglot.Common.DTOs.NoSQL
{
    public class ComplexStringDTO
    {
		public int Id { get; set; }

		public int ProjectId { get; set; }
		public string Language { get; set; }
		public string OriginalValue { get; set; }
		public string Description { get; set; }
		public string PictureLink { get; set; }

		public List<TranslationDTO> Translations { get; set; }
		public List<CommentDTO> Comments { get; set; }
		public List<string> Tags { get; set; }

		public ComplexStringDTO()
		{

		}
	}
}
