using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;

namespace TODO.Application.CQRS.Queries.GetIncomingProjectTask
{
    public class GetIncomingProjectTaskQuery : IRequest<IEnumerable<ProjectTaskDTO>>
    {

    }
}