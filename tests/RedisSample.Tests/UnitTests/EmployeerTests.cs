using RedisSample.DataDomain.Models;
using System;
using Xunit;

namespace RedisSample.UnitTests.Tests
{
    public class EmployerTests    
    {
        private Employer employer;
        private Employer inactiveEmployer;

        public EmployerTests()
        {
            this.employer = new Employer("Test");
            this.inactiveEmployer = new Employer("Inactive");
            this.inactiveEmployer.InactivateEmployer();
        }

        [Fact]
        public void Employer_CreateEmployer_ShouldReturnStatusActive()
        {
            Assert.True(employer.IsActive());
        }

        [Fact]
        public void Employer_ChangeEmployerToInactive_ShouldReturnStatusInactive()
        {
            employer.InactivateEmployer();
            Assert.False(employer.IsActive());
        }

        [Fact]
        public void Employer_ChangeEmployerToActive_ShouldReturnStatusActive()
        {
            inactiveEmployer.ActivateEmployer();
            Assert.True(employer.IsActive());
        }
    }
}
