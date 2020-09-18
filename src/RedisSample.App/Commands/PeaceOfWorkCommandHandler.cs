using MediatR;
using RedisSample.DataDomain.Interfaces;
using RedisSample.DataDomain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RedisSample.App.Commands
{
    public class PeaceOfWorkCommandHandler : IRequestHandler<AddPeaceOfWorkCommand, bool>
    {
        private readonly IPieceOfWorkRepository _repository;
        private readonly IMediator _mediator;

        public PeaceOfWorkCommandHandler(IPieceOfWorkRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AddPeaceOfWorkCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return false;

            var pow = new PieceOfWork(request.Name, request.CreatedAt, request.Employee);

            await _repository.Add(pow);

            await _repository.UnitOfWork.Commit();

            return true;
        }
    }
}
