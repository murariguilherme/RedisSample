using RedisSample.DataDomain.Extensions;
using RedisSample.DataDomain.Models;
using System;
using Xunit;

namespace RedisSample.UnitTests.Tests
{
    public class PieceOfWorkTests
    {
        private Employer employer;
        private Employer inactiveEmployer;
        private PieceOfWork powWithEmployer;
        private PieceOfWork powWithNoEmployer;

        public PieceOfWorkTests()
        {
            employer = new Employer("Test");
            inactiveEmployer = new Employer("Inactive employer");
            inactiveEmployer.InactivateEmployer();

            powWithEmployer = new PieceOfWork("Do something", DateTime.Now, new Employer("Test name"));
            powWithNoEmployer = new PieceOfWork("Do something", DateTime.Now);
        }

        [Fact]
        public void PoW_Complete_ShouldHaveStatusCompleted()
        {
            powWithEmployer.CompletePow();

            Assert.True(powWithEmployer.IsCompleted());
        }

        [Fact]
        public void PoW_Complete_ShouldHaveThrowDomainException()
        {
            var ex = Assert.Throws<DomainException>(() => powWithNoEmployer.CompletePow());
            Assert.Equal("To complete a piece of work, it should have a employer", ex.Message);
            
            powWithEmployer.RemoveEmployer();
            ex = Assert.Throws<DomainException>(() => powWithEmployer.CompletePow());
            Assert.Equal("To complete a piece of work, it should have a employer", ex.Message);
        }

        [Fact]
        public void Pow_ChangeEmployer_ShouldReturnTrueHasEmployer()
        {
            Assert.True(powWithEmployer.HasEmployer());            
            
            powWithEmployer.ChangeEmployer(employer);
            Assert.True(powWithEmployer.HasEmployer());
        }

        [Fact]
        public void Pow_ChangeEmployer_ShouldReturnFalseHasEmployer()
        {
            Assert.False(powWithNoEmployer.HasEmployer());
            
            powWithEmployer.RemoveEmployer();
            Assert.False(powWithEmployer.HasEmployer());
        }

        [Fact]
        public void Pow_ChangeEmployerPoWCompleted_ShouldReturnDomainException()
        {
            powWithEmployer.CompletePow();            
            var ex = Assert.Throws<DomainException>(() => powWithEmployer.ChangeEmployer(employer));

            Assert.Equal("You cannot change a employer of a completed piece of work", ex.Message);
        }

        [Fact]
        public void Pow_ChangeToInactiveEmployer_ShouldReturnDomainException()
        {                        
            var ex = Assert.Throws<DomainException>(() => powWithEmployer.ChangeEmployer(inactiveEmployer));

            Assert.Equal("You cannot change to a inactive employer", ex.Message);
        }

        [Fact]
        public void PoW_UndoComplete_ShouldCompletedStatusBeTrue()
        {
            powWithEmployer.CompletePow();

            Assert.True(powWithEmployer.IsCompleted());
        }

        [Fact]
        public void PoW_UndoComplete_ShouldCompletedStatusBeFalse()
        {
            Assert.False(powWithEmployer.IsCompleted());
        }

        [Fact]
        public void PoW_UndoComplete_ShouldCompletedStatusBeFalseWhenUndoCompleted()
        {
            powWithEmployer.CompletePow();
            powWithEmployer.UndoCompletePow();

            Assert.False(powWithEmployer.IsCompleted());
        }

        [Fact]
        public void PoW_ChangeName_ShouldChangeSucessfully()
        {
            powWithEmployer.ChangePieceOfWorkName("New name");

            Assert.Equal("New name", powWithEmployer.Name);
        }

        [Fact]
        public void PoW_ChangeName_ShouldThrowDomainException()
        {
            powWithEmployer.CompletePow();

            var ex = Assert.Throws<DomainException>(() => powWithEmployer.ChangePieceOfWorkName("New name"));
            Assert.Equal("You cannot change a name of a completed piece of work", ex.Message);            
        }
    }
}
