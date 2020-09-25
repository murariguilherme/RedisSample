using System;
using System.Collections.Generic;
using System.Text;
using RedisSample.DataDomain.Extensions;

namespace RedisSample.DataDomain.Models
{
    [Serializable]
    public class PieceOfWork: Entity
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public bool Completed { get; set; }

        public PieceOfWork(string name, DateTime createdAt, Employee employee)
        {
            this.Name = name;
            this.CreatedAt = createdAt;
            this.Employee = employee;
            this.Completed = false;
        }

        public PieceOfWork(string name, DateTime createdAt)
        {
            this.CreatedAt = createdAt;            
            this.Completed = false;
        }

        public PieceOfWork() { }

        public void ChangePieceOfWorkName(string name)
        {
            if (this.IsCompleted()) throw new DomainException("You cannot change a name of a completed piece of work");

            this.Name = name;
        }

        public void ChangeEmployee(Employee employee)
        {
            if (!employee.IsActive()) throw new DomainException("You cannot change to a inactive employee");
            if (this.IsCompleted()) throw new DomainException("You cannot change a employee of a completed piece of work");

            this.Employee = employee;            
        }

        public void RemoveEmployee()
        {            
            this.Employee = null;
        }

        public bool IsCompleted()
        {
            return this.Completed;
        }

        public void CompletePow()
        {
            if (!this.HasEmployee()) throw new DomainException("To complete a piece of work, it should have a employee");
            this.Completed = true;
        }

        public void UndoCompletePow()
        {
            this.Completed = false;
        }
        public bool HasEmployee()
        {
            return (this.Employee != null);
        }
    }
}
