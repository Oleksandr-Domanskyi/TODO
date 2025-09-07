using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Application.CQRS.Queries.GetAllProjectTasks
{
    public class GetAllProjectTasksQueryHandle : IRequestHandler<GetAllProjectTasksQuery, IEnumerable<ProjectTaskDTO>>
    {
        private readonly IProjectTaskRepositoryServices _projectTaskRepository;
        public GetAllProjectTasksQueryHandle(IProjectTaskRepositoryServices projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }
        public async Task<IEnumerable<ProjectTaskDTO>> Handle(GetAllProjectTasksQuery request, CancellationToken cancellationToken)
        {
            var model = await _projectTaskRepository.GetAllProjectTasks();
            if (model.IsSuccess)
            {
                return model.Value;
            }
            throw new Exception(string.Join(",", model.Errors.Select(error => error.Message)));
        }
    }
}