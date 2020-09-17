using RedisSample.DataDomain.Extensions;
using RedisSample.DataDomain.Models;
using System;
using Xunit;

namespace RedisSample.UnitTests.Tests
{
    public class PieceOfWorkTests
    {
        private Employeer employeer;
        private Employeer inactiveEmployeer;
        private PieceOfWork powWithEmployeer;
        private PieceOfWork powWithNoEmployeer;

        public PieceOfWorkTests()
        {
            employeer = new Employeer("Test");
            inactiveEmployeer = new Employeer("Inactive employeer");
            inactiveEmployeer.InactivateEmployeer();

            powWithEmployeer = new PieceOfWork("Do something", DateTime.Now, new Employeer("Test name"));
            powWithNoEmployeer = new PieceOfWork("Do something", DateTime.Now);
        }

        [Fact]
        public void PoW_Complete_ShouldHaveStatusCompleted()
        {
            powWithEmployeer.CompletePow();

            Assert.True(powWithEmployeer.IsCompleted());
        }

        [Fact]
        public void PoW_Complete_ShouldHaveThrowDomainException()
        {
            var ex = Assert.Throws<DomainException>(() => powWithNoEmployeer.CompletePow());
            Assert.Equal("To complete a piece of work, it should have a employeer", ex.Message);
            
            powWithEmployeer.RemoveEmployeer();
            ex = Assert.Throws<DomainException>(() => powWithEmployeer.CompletePow());
            Assert.Equal("To complete a piece of work, it should have a employeer", ex.Message);
        }

        [Fact]
        public void Pow_ChangeEmployeer_ShouldReturnTrueHasEmployeer()
        {
            Assert.True(powWithEmployeer.HasEmployeer());            
            
            powWithEmployeer.ChangeEmployeer(employeer);
            Assert.True(powWithEmployeer.HasEmployeer());
        }

        [Fact]
        public void Pow_ChangeEmployeer_ShouldReturnFalseHasEmployeer()
        {
            Assert.False(powWithNoEmployeer.HasEmployeer());
            
            powWithEmployeer.RemoveEmployeer();
            Assert.False(powWithEmployeer.HasEmployeer());
        }

        [Fact]
        public void Pow_ChangeEmployeerPoWCompleted_ShouldReturnDomainException()
        {
            powWithEmployeer.CompletePow();            
            var ex = Assert.Throws<DomainException>(() => powWithEmployeer.ChangeEmployeer(employeer));

            Assert.Equal("You cannot change a employeer of a completed piece of work", ex.Message);
        }

        [Fact]
        public void Pow_ChangeToInactiveEmployeer_ShouldReturnDomainException()
        {                        
            var ex = Assert.Throws<DomainException>(() => powWithEmployeer.ChangeEmployeer(inactiveEmployeer));

            Assert.Equal("You cannot change to a inactive employeer", ex.Message);
        }

        [Fact]
        public void PoW_UndoComplete_ShouldCompletedStatusBeTrue()
        {
            powWithEmployeer.CompletePow();

            Assert.True(powWithEmployeer.IsCompleted());
        }

        [Fact]
        public void PoW_UndoComplete_ShouldCompletedStatusBeFalse()
        {
            Assert.False(powWithEmployeer.IsCompleted());
        }

        [Fact]
        public void PoW_UndoComplete_ShouldCompletedStatusBeFalseWhenUndoCompleted()
        {
            powWithEmployeer.CompletePow();
            powWithEmployeer.UndoCompletePow();

            Assert.False(powWithEmployeer.IsCompleted());
        }

        [Fact]
        public void PoW_ChangeName_ShouldChangeSucessfully()
        {
            powWithEmployeer.ChangePieceOfWorkName("New name");

            Assert.Equal("New name", powWithEmployeer.Name);
        }

        [Fact]
        public void PoW_ChangeName_ShouldThrowDomainException()
        {
            powWithEmployeer.CompletePow();

            var ex = Assert.Throws<DomainException>(() => powWithEmployeer.ChangePieceOfWorkName("New name"));
            Assert.Equal("You cannot change a name of a completed piece of work", ex.Message);            
        }
    }
}
