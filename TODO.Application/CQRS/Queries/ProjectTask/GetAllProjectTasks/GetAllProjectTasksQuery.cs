using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;

namespace TODO.Application.CQRS.Queries.GetAllProjectTasks
{
    public class GetAllProjectTasksQuery : IRequest<IEnumerable<ProjectTaskDTO>>
    {

    }
}