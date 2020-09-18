using RedisSample.DataDomain.Extensions;
using RedisSample.DataDomain.Models;
using System;
using Xunit;

namespace RedisSample.UnitTests.Tests
{
    public class PieceOfWorkTests
    {
        private Employee employee;
        private Employee inactiveEmployee;
        private PieceOfWork powWithEmployee;
        private PieceOfWork powWithNoEmployee;

        public PieceOfWorkTests()
        {
            employee = new Employee("Test");
            inactiveEmployee = new Employee("Inactive employee");
            inactiveEmployee.InactivateEmployee();

            powWithEmployee = new PieceOfWork("Do something", DateTime.Now, new Employee("Test name"));
            powWithNoEmployee = new PieceOfWork("Do something", DateTime.Now);
        }

        [Fact]
        public void PoW_Complete_ShouldHaveStatusCompleted()
        {
            powWithEmployee.CompletePow();

            Assert.True(powWithEmployee.IsCompleted());
        }

        [Fact]
        public void PoW_Complete_ShouldHaveThrowDomainException()
        {
            var ex = Assert.Throws<DomainException>(() => powWithNoEmployee.CompletePow());
            Assert.Equal("To complete a piece of work, it should have a employee", ex.Message);
            
            powWithEmployee.RemoveEmployee();
            ex = Assert.Throws<DomainException>(() => powWithEmployee.CompletePow());
            Assert.Equal("To complete a piece of work, it should have a employee", ex.Message);
        }

        [Fact]
        public void Pow_ChangeEmployee_ShouldReturnTrueHasEmployee()
        {
            Assert.True(powWithEmployee.HasEmployee());            
            
            powWithEmployee.ChangeEmployee(employee);
            Assert.True(powWithEmployee.HasEmployee());
        }

        [Fact]
        public void Pow_ChangeEmployee_ShouldReturnFalseHasEmployee()
        {
            Assert.False(powWithNoEmployee.HasEmployee());
            
            powWithEmployee.RemoveEmployee();
            Assert.False(powWithEmployee.HasEmployee());
        }

        [Fact]
        public void Pow_ChangeEmployeePoWCompleted_ShouldReturnDomainException()
        {
            powWithEmployee.CompletePow();            
            var ex = Assert.Throws<DomainException>(() => powWithEmployee.ChangeEmployee(employee));

            Assert.Equal("You cannot change a employee of a completed piece of work", ex.Message);
        }

        [Fact]
        public void Pow_ChangeToInactiveEmployee_ShouldReturnDomainException()
        {                        
            var ex = Assert.Throws<DomainException>(() => powWithEmployee.ChangeEmployee(inactiveEmployee));

            Assert.Equal("You cannot change to a inactive employee", ex.Message);
        }

        [Fact]
        public void PoW_UndoComplete_ShouldCompletedStatusBeTrue()
        {
            powWithEmployee.CompletePow();

            Assert.True(powWithEmployee.IsCompleted());
        }

        [Fact]
        public void PoW_UndoComplete_ShouldCompletedStatusBeFalse()
        {
            Assert.False(powWithEmployee.IsCompleted());
        }

        [Fact]
        public void PoW_UndoComplete_ShouldCompletedStatusBeFalseWhenUndoCompleted()
        {
            powWithEmployee.CompletePow();
            powWithEmployee.UndoCompletePow();

            Assert.False(powWithEmployee.IsCompleted());
        }

        [Fact]
        public void PoW_ChangeName_ShouldChangeSucessfully()
        {
            powWithEmployee.ChangePieceOfWorkName("New name");

            Assert.Equal("New name", powWithEmployee.Name);
        }

        [Fact]
        public void PoW_ChangeName_ShouldThrowDomainException()
        {
            powWithEmployee.CompletePow();

            var ex = Assert.Throws<DomainException>(() => powWithEmployee.ChangePieceOfWorkName("New name"));
            Assert.Equal("You cannot change a name of a completed piece of work", ex.Message);            
        }
    }
}
