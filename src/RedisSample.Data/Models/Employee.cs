using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using RedisSample.DataDomain.Extensions;

namespace RedisSample.DataDomain.Models
{
    [Serializable]
    public class Employee: Entity
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        [JsonIgnore]
        public List<PieceOfWork> PiecesOfWork { get; set; }

        public Employee(string name)
        {
            this.Name = name;
            this.Active = true;
        }

        public Employee()
        {

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
