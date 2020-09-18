using Moq;
using Moq.AutoMock;
using RedisSample.App.Commands;
using RedisSample.DataDomain.Interfaces;
using RedisSample.DataDomain.Models;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RedisSample.Tests.TDD
{
    public class PeaceOfWorkCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly PeaceOfWorkCommandHandler _pedidoHandler;
        private Employer employer;
        private AddPeaceOfWorkCommand validCommand;
        private AddPeaceOfWorkCommand invalidCommand;

        public PeaceOfWorkCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _pedidoHandler = _mocker.CreateInstance<PeaceOfWorkCommandHandler>();
            this.employer = new Employer("Test");
            this.validCommand = new AddPeaceOfWorkCommand("Do something", DateTime.Now, employer);
            this.invalidCommand = new AddPeaceOfWorkCommand("", DateTime.Now, employer);
        }

        [Fact]
        public async Task Add_NewPeaceOfWorkValid_ShouldIsertInDatabaseAsync()
        {
            _mocker.GetMock<IPieceOfWorkRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _pedidoHandler.Handle(validCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IPieceOfWorkRepository>().Verify(r => r.Add(It.IsAny<PieceOfWork>()), Times.Once);
            _mocker.GetMock<IPieceOfWorkRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task Add_NewPeaceOfWorkInvalid_ShouldIsertInDatabaseAsync()
        {
            _mocker.GetMock<IPieceOfWorkRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _pedidoHandler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            Assert.False(result);           
        }

    }
}
