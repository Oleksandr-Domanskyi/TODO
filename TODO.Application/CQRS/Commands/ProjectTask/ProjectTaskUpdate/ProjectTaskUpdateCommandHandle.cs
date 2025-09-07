using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Application.CQRS.Commands.ProjectTaskCreate;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Application.CQRS.Commands.ProjectTaskUpdate
{
    public class ProjectTaskUpdateCommandHandler : IRequestHandler<ProjectTaskUpdateCommand, ProjectTaskDTO>
    {
        private readonly IProjectTaskRepositoryServices _services;

        public ProjectTaskUpdateCommandHandler(IProjectTaskRepositoryServices services)
        {
            _services = services;
        }

        public async Task<ProjectTaskDTO> Handle(ProjectTaskUpdateCommand request, CancellationToken cancellationToken)
        {
            var result = await _services.UpdateProjectTask(request.Id, request.ProjectTaskUpdateDTO);

            if (result.IsSuccess)
                return result.Value;

            throw new InvalidOperationException("Failed to update ProjectTask: " + string.Join(", ", result.Errors));
        }
    }

}