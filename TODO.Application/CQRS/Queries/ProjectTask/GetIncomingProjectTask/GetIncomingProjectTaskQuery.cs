using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;
using TODO.Core.Enums;

namespace TODO.Application.CQRS.Queries.GetIncomingProjectTask
{
    public class GetIncomingProjectTaskQuery : IRequest<IEnumerable<ProjectTaskDTO>>
    {

        public TaskPeriod Period { get; }
        public GetIncomingProjectTaskQuery(TaskPeriod period)
        {
            this.Period = period;
        }
    }
}