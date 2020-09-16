using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSample.DataDomain.Models
{
    public class PieceOfWork: Entity
    {
        public DateTime CreatedAt { get; private set; }
        public Guid EmployeerId { get; private set; }
        public Employeer Employeer { get; private set; }
        public bool Completed { get; private set; }

        public PieceOfWork(DateTime createdAt, Guid employeerId)
        {
            this.CreatedAt = createdAt;
            this.EmployeerId = employeerId;
            this.Completed = false;
        }
    }
}
