using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;

namespace TODO.Application.CQRS.Queries.GetProjectTaskById
{
    public class GetProjectTaskByIdQuery : IRequest<ProjectTaskDTO>
    {
        public Guid _Id { get; set; }
        public GetProjectTaskByIdQuery(Guid Id)
        {
            _Id = Id;

        }
    }
}