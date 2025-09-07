using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace TODO.Application.CQRS.Commands.SetProjectTaskPercent
{
    public class SetAsComplateProjectTaskPercentCommand : IRequest<Unit>
    {
        public Guid Id { get; }
        public SetAsComplateProjectTaskPercentCommand(Guid id)
        {
            this.Id = id;
        }
    }
}