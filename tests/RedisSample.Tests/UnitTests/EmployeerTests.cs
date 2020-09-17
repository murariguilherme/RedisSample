using RedisSample.DataDomain.Models;
using System;
using Xunit;

namespace RedisSample.UnitTests.Tests
{
    public class EmployeerTests    
    {
        private Employeer employeer;
        private Employeer inactiveEmployeer;

        public EmployeerTests()
        {
            this.employeer = new Employeer("Test");
            this.inactiveEmployeer = new Employeer("Inactive");
            this.inactiveEmployeer.InactivateEmployeer();
        }

        [Fact]
        public void Employeer_CreateEmployeer_ShouldReturnStatusActive()
        {
            Assert.True(employeer.IsActive());
        }

        [Fact]
        public void Employeer_ChangeEmployeerToInactive_ShouldReturnStatusInactive()
        {
            employeer.InactivateEmployeer();
            Assert.False(employeer.IsActive());
        }

        [Fact]
        public void Employeer_ChangeEmployeerToActive_ShouldReturnStatusActive()
        {
            inactiveEmployeer.ActivateEmployeer();
            Assert.True(employeer.IsActive());
        }
    }
}
