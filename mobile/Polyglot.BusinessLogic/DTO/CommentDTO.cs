using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.BusinessLogic.DTO
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public UserProfileDTO User { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
