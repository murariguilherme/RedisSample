using System;
using System.Collections.Generic;
using RedisSample.DataDomain.Extensions;

namespace RedisSample.DataDomain.Models
{
    [Serializable]
    public class Employee: Entity
    {
        public string Name { get; private set; }
        public bool Active { get; private set; }
        public List<PieceOfWork> PiecesOfWork { get; private set; }

        public Employee(string name)
        {
            this.Name = Name;
            this.Active = true;
        }

        public void ActivateEmployee() 
        {
            this.Active = true;
        }

        public void InactivateEmployee() 
        {
            this.Active = false;
        }

        public bool IsActive()
        {
            return this.Active;
        }
    }
}
