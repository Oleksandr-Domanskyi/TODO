using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TODO.Infrastructure.Services.IServices;
using FluentResults;
using TODO.Core.Dto;

namespace TODO.Application.CQRS.Commands.SubTask.SubTaskUpdate
{
    public class SubTaskUpdateCommandHandler : IRequestHandler<SubTaskUpdateCommand, SubTaskDTO>
    {
        private readonly ISubTaskRepositoryServices _subTaskService;

        public SubTaskUpdateCommandHandler(ISubTaskRepositoryServices subTaskService)
        {
            _subTaskService = subTaskService;
        }

        public async Task<SubTaskDTO> Handle(SubTaskUpdateCommand request, CancellationToken cancellationToken)
        {
            var result = await _subTaskService.UpdateSubTask(request.Id, request.SubTask);

            if (result.IsFailed)
                throw new ApplicationException(result.Errors[0].Message);

            return result.Value;
        }
    }
}
