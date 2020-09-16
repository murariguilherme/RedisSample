using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSample.DataDomain.Models
{
    public class Employeer: Entity
    {
        public string Name { get; private set; }
        public List<PieceOfWork> PiecesOfWork { get; private set; }

        public Employeer(string name)
        {
            this.Name = Name;
        }
    }
}
