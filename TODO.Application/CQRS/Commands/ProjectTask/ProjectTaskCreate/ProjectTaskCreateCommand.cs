using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;

namespace TODO.Application.CQRS.Commands.ProjectTaskCreate
{
    public class ProjectTaskCreateCommand : IRequest<ProjectTaskDTO>
    {
        public ProjectTaskCreateDTO ProjectTaskCreateDTO { get; }
        public ProjectTaskCreateCommand(ProjectTaskCreateDTO projectTaskCreateDTO)
        {
            this.ProjectTaskCreateDTO = projectTaskCreateDTO;
        }

    }
}