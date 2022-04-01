using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.Model
{
    public class Entity
    {
        private Guid equalsGuid;

        public virtual long Id { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModificationDate { get; set; }

        public Entity()
        {
            equalsGuid = Guid.NewGuid();

            CreateDate = DateTime.UtcNow;
            ModificationDate = DateTime.UtcNow;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if (obj is Entity)
            {
                var e = obj as Entity;
                return e.GetHashCode().Equals(GetHashCode()) && obj.GetType() == GetType();
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (IdIsDefaultValue())
            {
                return equalsGuid.GetHashCode();
            }

            long code = Id.GetHashCode();

            return (int)(code % int.MaxValue);
        }

        public override string ToString()
        {
            return base.ToString() + "#" + Id;
        }

        private bool IdIsDefaultValue()
        {
            return Id.GetType().IsValueType && Id.GetHashCode() == Activator.CreateInstance(Id.GetType()).GetHashCode();
        }
    }
}
