using System;
using System.Collections.Generic;
using RedisSample.DataDomain.Extensions;

namespace RedisSample.DataDomain.Models
{
    public class Employeer: Entity
    {
        public string Name { get; private set; }
        public bool Active { get; private set; }
        public List<PieceOfWork> PiecesOfWork { get; private set; }

        public Employeer(string name)
        {
            this.Name = Name;
            this.Active = true;
        }

        public void ActivateEmployeer() 
        {
            this.Active = true;
        }

        public void InactivateEmployeer() 
        {
            this.Active = false;
        }

        public bool IsActive()
        {
            return this.Active;
        }
    }
}
