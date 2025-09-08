using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Application.CQRS.Commands.SubTask.SetAsComplateSubTask
{
    public class SetAsComplateSubTaskCommandHandler : IRequestHandler<SetAsComplateSubTaskCommand, Unit>
    {
        private readonly ISubTaskRepositoryServices _subTaskRepositoryServices;

        public SetAsComplateSubTaskCommandHandler(ISubTaskRepositoryServices subTaskRepositoryServices)
        {
            _subTaskRepositoryServices = subTaskRepositoryServices;
        }

        public async Task<Unit> Handle(SetAsComplateSubTaskCommand request, CancellationToken cancellationToken)
        {
            var result = await _subTaskRepositoryServices.MarkAsDone(request.Id);

            if (result.IsFailed)
                throw new Exception($"Failed to set SubTask {request.Id} as completed");

            return Unit.Value;
        }
    }
}
