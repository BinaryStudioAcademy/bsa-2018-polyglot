namespace Polyglot.DataAccess.Entities
{
    public class Manager : Entity
    {
        public UserProfile UserProfile { get; set; }
		
		public List<Project> Projects { get; set; }
    }
}