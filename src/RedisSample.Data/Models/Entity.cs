using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSample.Data.Models
{
    public abstract class Entity
    {
        public Guid Id { get; }

        protected Entity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
