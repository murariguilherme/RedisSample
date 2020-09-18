﻿using System;
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
        private Employer employer;
        private AddPeaceOfWorkCommand validCommand;
        private AddPeaceOfWorkCommand invalidCommand;

        public AddPeaceOfWorkCommandTests()
        {
            this.employer = new Employer("Test");
            this.validCommand = new AddPeaceOfWorkCommand("Do something", DateTime.Now, employer);
            this.invalidCommand = new AddPeaceOfWorkCommand("", DateTime.Now, null);
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
        }
    }
}
