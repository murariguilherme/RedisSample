using System;
using System.Collections.Generic;
using RedisSample.DataDomain.Extensions;

namespace RedisSample.DataDomain.Models
{
    [Serializable]
    public class Employer: Entity
    {
        public string Name { get; private set; }
        public bool Active { get; private set; }
        public List<PieceOfWork> PiecesOfWork { get; private set; }

        public Employer(string name)
        {
            this.Name = Name;
            this.Active = true;
        }

        public void ActivateEmployer() 
        {
            this.Active = true;
        }

        public void InactivateEmployer() 
        {
            this.Active = false;
        }

        public bool IsActive()
        {
            return this.Active;
        }
    }
}
