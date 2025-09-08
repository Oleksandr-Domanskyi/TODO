using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Application.CQRS.Queries.SubTask.GetAllSubTask
{
    public class GetAllSubTasksQueryHandler : IRequestHandler<GetAllSubTasksQuery, IEnumerable<SubTaskDTO>>
    {
        private readonly ISubTaskRepositoryServices _subTaskService;

        public GetAllSubTasksQueryHandler(ISubTaskRepositoryServices subTaskService)
        {
            _subTaskService = subTaskService;
        }

        public async Task<IEnumerable<SubTaskDTO>> Handle(GetAllSubTasksQuery request, CancellationToken cancellationToken)
        {
            var result = await _subTaskService.GetAllSubTasks();
            if (result.IsFailed)
                throw new ApplicationException(result.Errors[0].Message);

            return result.Value;
        }
    }
}
