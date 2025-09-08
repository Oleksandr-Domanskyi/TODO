using System.Collections.Generic;
using MediatR;
using TODO.Core.Dto;

namespace TODO.Application.CQRS.Queries.SubTask.GetAllSubTask
{
    public class GetAllSubTasksQuery : IRequest<IEnumerable<SubTaskDTO>>
    {

    }
}
