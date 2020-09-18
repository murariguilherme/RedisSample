using FluentValidation;
using MediatR;
using RedisSample.DataDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSample.App.Commands
{
    public class AddPeaceOfWorkCommand: Command
    {
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public Employee? Employee { get; private set; }
        
        public AddPeaceOfWorkCommand(string name, DateTime createdAt, Employee employee)
        {
            this.Name = name;
            this.CreatedAt = createdAt;
            this.Employee = employee;
            this.EmployeeId = employee?.Id;
        }

        public bool IsValid()
        {
            ValidationResult = new AddPeaceOfWorkCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class AddPeaceOfWorkCommandValidation: AbstractValidator<AddPeaceOfWorkCommand>
    {
        public static string NameEmptyErrorMsg => "You cannot add a piece of work without a name";
        public static string CompletedErrorMsg => "You cannot add a piece of work already completed";
        public AddPeaceOfWorkCommandValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage(NameEmptyErrorMsg);
        }
    }
}
