using System;

namespace Polyglot.Common.DTOs
{
    public class ProjectHistoryDTO
    {
        public int Id { get; set; }  
        public ProjectDTO Project { get; set; }
        public UserProfileDTO Actor { get; set; }
        public string TableName { get; set; }
        public string ActionType { get; set; }
        public string OriginValue { get; set; }
        public DateTime Time { get; set; }
    }
}
