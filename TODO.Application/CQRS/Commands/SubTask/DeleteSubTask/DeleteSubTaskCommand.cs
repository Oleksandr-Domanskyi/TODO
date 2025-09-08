using System;
using MediatR;

namespace TODO.Application.CQRS.Commands.SubTask.DeleteSubTask
{
    public class DeleteSubTaskCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteSubTaskCommand(Guid id)
        {
            Id = id;
        }
    }
}
