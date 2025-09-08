using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TODO.Infrastructure.Services.IServices;
using TODO.Core.Dto;

namespace TODO.Application.CQRS.Commands.SubTask.SubTaskCreate
{
    public class SubTaskCreateCommandHandler : IRequestHandler<SubTaskCreateCommand, SubTaskDTO>
    {
        private readonly ISubTaskRepositoryServices _subTaskService;

        public SubTaskCreateCommandHandler(ISubTaskRepositoryServices subTaskService)
        {
            _subTaskService = subTaskService;
        }

        public async Task<SubTaskDTO> Handle(SubTaskCreateCommand request, CancellationToken cancellationToken)
        {
            var result = await _subTaskService.AddSubTask(request.ProjectTaskId, request.SubTask);

            if (result.IsFailed)
                throw new ApplicationException(result.Errors[0].Message);

            return result.Value;
        }
    }
}
