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
        public Guid EmployeerId { get; private set; }
        public Employeer Employeer { get; private set; }
        public bool Completed { get; private set; }

        public PieceOfWork(DateTime createdAt, Guid employeerId)
        {
            this.CreatedAt = createdAt;
            this.EmployeerId = employeerId;
            this.Completed = false;
        }

        public void ChangePieceOfWorkName(string name)
        {
            if (this.IsCompleted()) throw new DomainException();

            this.Name = name;
        }

        public void ChangeEmployeer(Employeer employeer)
        {
            if (this.IsCompleted()) throw new DomainException();

            this.Employeer = employeer;
        }

        public bool IsCompleted()
        {
            return this.Completed;
        }

        public void CompleteTask()
        {
            this.Completed = true;
        }

        public void UndoCompleteTask()
        {
            this.Completed = false;
        }
    }
}
