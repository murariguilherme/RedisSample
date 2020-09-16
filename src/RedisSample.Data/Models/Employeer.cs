using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSample.Data.Models
{
    public class Employeer: Entity
    {
        public string Name { get; private set; }
        public List<Task> Tasks { get; private set; }

        public Employeer(string name)
        {
            this.Name = Name;
        }
    }
}
