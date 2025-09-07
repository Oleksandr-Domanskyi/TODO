using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Application.CQRS.Queries.GetProjectTaskById
{
    public class GetProjectTaskByIdQueryHandle : IRequestHandler<GetProjectTaskByIdQuery, ProjectTaskDTO>
    {
        private readonly IProjectTaskRepositoryServices _projectTaskRepositoryServices;
        public GetProjectTaskByIdQueryHandle(IProjectTaskRepositoryServices projectTaskRepositoryServices)
        {
            _projectTaskRepositoryServices = projectTaskRepositoryServices;
        }

        public async Task<ProjectTaskDTO> Handle(GetProjectTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _projectTaskRepositoryServices.GetProjectTaskById(request._Id);
            if (model.IsSuccess)
            {
                return model.Value;
            }
            throw new Exception(string.Join(", ", model.Errors.Select(e => e.Message)));
        }
    }
}