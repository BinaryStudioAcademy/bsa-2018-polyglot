using Polyglot.DataAccess.Entities;
using System;

namespace Polyglot.Common.DTOs.NoSQL
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public UserProfileDTO User { get; set; }
		public string Text { get; set; }
		public DateTime CreatedOn { get; set; }

		public CommentDTO()
		{

		}
	}
}
