using System;

namespace Polyglot.Common.DTOs.NoSQL
{
    public class CommentDTO
    {
		public int UserId { get; set; }
		public string Text { get; set; }
		public DateTime CreatedOn { get; set; }

		public CommentDTO()
		{

		}
	}
}
