using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Application.CQRS.Commands.ProjectTaskCreate
{
    public class ProjectTaskCreateCommandHandle : IRequestHandler<ProjectTaskCreateCommand, ProjectTaskDTO>
    {
        private readonly IProjectTaskRepositoryServices _projectTaskRepositoryServices;

        public ProjectTaskCreateCommandHandle(IProjectTaskRepositoryServices projectTaskRepositoryServices)
        {
            _projectTaskRepositoryServices = projectTaskRepositoryServices;
        }
        public async Task<ProjectTaskDTO> Handle(ProjectTaskCreateCommand request, CancellationToken cancellationToken)
        {
            var model = await _projectTaskRepositoryServices.AddProjectTask(request.ProjectTaskCreateDTO);
            if (model.IsSuccess)
                return model.Value;
            throw new Exception(string.Join(",", model.Errors.Select(error => error.Message)));
        }
    }
}