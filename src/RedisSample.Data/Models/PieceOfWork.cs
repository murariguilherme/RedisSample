using System;
using System.Collections.Generic;
using System.Text;
using RedisSample.DataDomain.Extensions;

namespace RedisSample.DataDomain.Models
{
    [Serializable]
    public class PieceOfWork: Entity
    {
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid? EmployerId { get; private set; }
        public Employer? Employer { get; private set; }
        public bool Completed { get; private set; }

        public PieceOfWork(string name, DateTime createdAt, Employer employer)
        {
            this.Name = name;
            this.CreatedAt = createdAt;
            this.Employer = employer;
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

        public void ChangeEmployer(Employer employer)
        {
            if (!employer.IsActive()) throw new DomainException("You cannot change to a inactive employer");
            if (this.IsCompleted()) throw new DomainException("You cannot change a employer of a completed piece of work");

            this.Employer = employer;            
        }

        public void RemoveEmployer()
        {            
            this.Employer = null;
        }

        public bool IsCompleted()
        {
            return this.Completed;
        }

        public void CompletePow()
        {
            if (!this.HasEmployer()) throw new DomainException("To complete a piece of work, it should have a employer");
            this.Completed = true;
        }

        public void UndoCompletePow()
        {
            this.Completed = false;
        }
        public bool HasEmployer()
        {
            return (this.Employer != null);
        }
    }
}
