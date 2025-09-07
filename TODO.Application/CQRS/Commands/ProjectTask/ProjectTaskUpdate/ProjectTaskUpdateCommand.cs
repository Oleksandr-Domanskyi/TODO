using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;

namespace TODO.Application.CQRS.Commands.ProjectTaskUpdate
{
    public class ProjectTaskUpdateCommand : IRequest<ProjectTaskDTO>
    {
        public ProjectTaskUpdateDTO ProjectTaskUpdateDTO { get; }
        public Guid Id { get; }
        public ProjectTaskUpdateCommand(Guid Id, ProjectTaskUpdateDTO projectTaskUpdateDTO)
        {
            this.Id = Id;
            this.ProjectTaskUpdateDTO = projectTaskUpdateDTO;
        }
    }
}