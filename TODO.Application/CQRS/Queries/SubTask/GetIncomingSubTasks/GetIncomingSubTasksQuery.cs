using System.Collections.Generic;
using MediatR;
using TODO.Core.Dto;
using TODO.Core.Enums;

namespace TODO.Application.CQRS.Queries.SubTask.GetIncomingSubTasks
{
    public class GetIncomingSubTasksQuery : IRequest<IEnumerable<SubTaskDTO>>
    {
        public TaskPeriod Period { get; }
        public GetIncomingSubTasksQuery(TaskPeriod period)
        {
            this.Period = period;
        }

    }
}
