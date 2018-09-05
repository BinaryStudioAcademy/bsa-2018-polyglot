namespace Polyglot.Common.DTOs
{
    public class TeamProjectDTO
    {
        public int Id { get; set; }
        public ProjectDTO Project { get; set; }
        public int ProjectId { get; set; }
        public int TeamId { get; set; }
    }
}