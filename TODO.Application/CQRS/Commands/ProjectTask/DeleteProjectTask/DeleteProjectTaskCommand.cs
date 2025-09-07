using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace TODO.Application.CQRS.Commands.DeleteProjectTask
{
    public class DeleteProjectTaskCommand : IRequest<Unit>
    {
        public Guid Id { get; }
        public DeleteProjectTaskCommand(Guid id)
        {
            this.Id = id;
        }

    }
}