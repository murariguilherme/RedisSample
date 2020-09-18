using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSample.App.Commands
{
    public abstract class Command: IRequest<bool>
    {
        public ValidationResult ValidationResult { get; set; } 
    }
}
