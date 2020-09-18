using RedisSample.DataDomain.Models;
using System;
using Xunit;

namespace RedisSample.UnitTests.Tests
{
    public class EmployeeTests    
    {
        private Employee employee;
        private Employee inactiveEmployee;

        public EmployeeTests()
        {
            this.employee = new Employee("Test");
            this.inactiveEmployee = new Employee("Inactive");
            this.inactiveEmployee.InactivateEmployee();
        }

        [Fact]
        public void Employee_CreateEmployee_ShouldReturnStatusActive()
        {
            Assert.True(employee.IsActive());
        }

        [Fact]
        public void Employee_ChangeEmployeeToInactive_ShouldReturnStatusInactive()
        {
            employee.InactivateEmployee();
            Assert.False(employee.IsActive());
        }

        [Fact]
        public void Employee_ChangeEmployeeToActive_ShouldReturnStatusActive()
        {
            inactiveEmployee.ActivateEmployee();
            Assert.True(employee.IsActive());
        }
    }
}
