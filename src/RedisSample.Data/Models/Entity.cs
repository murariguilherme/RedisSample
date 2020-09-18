using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSample.DataDomain.Models
{
    [Serializable]
    public abstract class Entity
    {
        public Guid Id { get; }

        protected Entity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
