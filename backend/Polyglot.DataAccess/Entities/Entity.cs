using System;

namespace Polyglot.DataAccess.Entities
{

    public class Entity : IEquatable<Entity>
    {
        public int Id { get; set; }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
    }
}
