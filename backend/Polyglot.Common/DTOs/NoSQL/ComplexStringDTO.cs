using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Polyglot.Common.DTOs.NoSQL
{
    public class ComplexStringDTO
    {
        [JsonProperty("id")]
		public int Id { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("projectId")]
        public int ProjectId { get; set; }
        [JsonProperty("languageId")]
        public int LanguageId { get; set; }
        [JsonProperty("base")]
        public string OriginalValue { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
		public string PictureLink { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        public List<TranslationDTO> Translations { get; set; }
		public List<CommentDTO> Comments { get; set; }
		public List<TagDTO> Tags { get; set; }

		public ComplexStringDTO()
		{

		}
	}
}
