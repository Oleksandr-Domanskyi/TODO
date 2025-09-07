using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Application.CQRS.Commands.DeleteProjectTask
{
    public class DeleteProjectTaskCommandHandle : IRequestHandler<DeleteProjectTaskCommand, Unit>
    {
        private readonly IProjectTaskRepositoryServices _projectTaskRepositoryServices;

        public DeleteProjectTaskCommandHandle(IProjectTaskRepositoryServices projectTaskRepositoryServices)
        {
            _projectTaskRepositoryServices = projectTaskRepositoryServices;
        }
        public async Task<Unit> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var model = await _projectTaskRepositoryServices.DeleteProjectTask(request.Id);
            if (!model.IsSuccess)
                throw new Exception("Set as complate project task has error");

            return Unit.Value;
        }
    }
}