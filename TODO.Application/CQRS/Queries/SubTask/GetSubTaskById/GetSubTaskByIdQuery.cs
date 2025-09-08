using System;
using MediatR;
using TODO.Core.Dto;

namespace TODO.Application.CQRS.Queries.SubTask.GetSubTaskById
{
    public class GetSubTaskByIdQuery : IRequest<SubTaskDTO>
    {
        public Guid Id { get; }

        public GetSubTaskByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
