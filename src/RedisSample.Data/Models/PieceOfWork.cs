using System;
using System.Collections.Generic;
using System.Text;
using RedisSample.DataDomain.Extensions;

namespace RedisSample.DataDomain.Models
{
    public class PieceOfWork: Entity
    {
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid? EmployeerId { get; private set; }
        public Employeer? Employeer { get; private set; }
        public bool Completed { get; private set; }

        public PieceOfWork(string name, DateTime createdAt, Employeer employeer)
        {
            this.CreatedAt = createdAt;
            this.Employeer = employeer;
            this.Completed = false;
        }

        public PieceOfWork(string name, DateTime createdAt)
        {
            this.CreatedAt = createdAt;            
            this.Completed = false;
        }

        public void ChangePieceOfWorkName(string name)
        {
            if (this.IsCompleted()) throw new DomainException("You cannot change a name of a completed piece of work");

            this.Name = name;
        }

        public void ChangeEmployeer(Employeer employeer)
        {
            if (!employeer.IsActive()) throw new DomainException("You cannot change to a inactive employeer");
            if (this.IsCompleted()) throw new DomainException("You cannot change a employeer of a completed piece of work");

            this.Employeer = employeer;            
        }

        public void RemoveEmployeer()
        {            
            this.Employeer = null;
        }

        public bool IsCompleted()
        {
            return this.Completed;
        }

        public void CompletePow()
        {
            if (!this.HasEmployeer()) throw new DomainException("To complete a piece of work, it should have a employeer");
            this.Completed = true;
        }

        public void UndoCompletePow()
        {
            this.Completed = false;
        }
        public bool HasEmployeer()
        {
            return (this.Employeer != null);
        }
    }
}
