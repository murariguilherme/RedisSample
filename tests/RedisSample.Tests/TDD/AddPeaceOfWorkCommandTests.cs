using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RedisSample.App.Commands;
using RedisSample.DataDomain.Models;
using System.Linq;

namespace RedisSample.Tests.TDD
{
    public class AddPeaceOfWorkCommandTests
    {
        private Employeer employeer;
        private AddPeaceOfWorkCommand validCommand;
        private AddPeaceOfWorkCommand invalidCommand;

        public AddPeaceOfWorkCommandTests()
        {
            this.employeer = new Employeer("Test");
            this.validCommand = new AddPeaceOfWorkCommand("Do something", DateTime.Now, employeer, false);
            this.invalidCommand = new AddPeaceOfWorkCommand("", DateTime.Now, null, true);
        }

        [Fact]
        public void AddPeaceOfWorkCommand_CommandIsValid_ShouldPassInValidation()
        {           
            var result = validCommand.IsValid();

            Assert.True(result);
        }

        [Fact]
        public void AddPeaceOfWorkCommand_CommandIsInvalid_ShouldNotPassInValidation()
        {
            var result = invalidCommand.IsValid();

            Assert.False(result);
            Assert.Contains(AddPeaceOfWorkCommandValidation.NameEmptyErrorMsg, invalidCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AddPeaceOfWorkCommandValidation.CompletedErrorMsg, invalidCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
