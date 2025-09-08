using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Core.Dto;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Application.CQRS.Queries.GetIncomingProjectTask
{
    public class GetIncomingProjectTaskQueryHandle : IRequestHandler<GetIncomingProjectTaskQuery, IEnumerable<ProjectTaskDTO>>
    {
        private readonly IProjectTaskRepositoryServices _projectTaskRepositoryServices;
        public GetIncomingProjectTaskQueryHandle(IProjectTaskRepositoryServices projectTaskRepositoryServices)
        {
            _projectTaskRepositoryServices = projectTaskRepositoryServices;
        }
        public async Task<IEnumerable<ProjectTaskDTO>> Handle(GetIncomingProjectTaskQuery request, CancellationToken cancellationToken)
        {
            var model = await _projectTaskRepositoryServices.GetIncomingProjectTaskAsync(request.Period);
            if (model.IsSuccess)
                return model.Value;
            throw new Exception(string.Join(", ", model.Errors.Select(e => e.Message)));
        }
    }
}