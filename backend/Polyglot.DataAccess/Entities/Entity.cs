using System;
using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{

    public class Entity : IEquatable<Entity>, IEqualityComparer<Entity>
    {
        public int Id { get; set; }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }

        public bool Equals(Entity x, Entity y)
        {
            if (x is null || y is null)
            return false;

            return x.Id == y.Id;
        }

        public int GetHashCode(Entity obj)
        {
            if (obj is null) return 0;

            unchecked
            {
                int hash = 17;
                return hash * 23 + obj.Id;
            }
        }
    }
}
