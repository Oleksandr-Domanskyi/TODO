using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Application.CQRS.Commands.SubTask.DeleteSubTask
{
    public class DeleteSubTaskCommandHandler : IRequestHandler<DeleteSubTaskCommand, Unit>
    {
        private readonly ISubTaskRepositoryServices _subTaskRepositoryServices;

        public DeleteSubTaskCommandHandler(ISubTaskRepositoryServices subTaskRepositoryServices)
        {
            _subTaskRepositoryServices = subTaskRepositoryServices;
        }

        public async Task<Unit> Handle(DeleteSubTaskCommand request, CancellationToken cancellationToken)
        {
            var result = await _subTaskRepositoryServices.DeleteSubTask(request.Id);

            if (!result.IsSuccess)
                throw new Exception($"Failed to delete SubTask with Id={request.Id}");

            return Unit.Value;
        }
    }
}
