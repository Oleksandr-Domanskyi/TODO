using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TODO.Infrastructure.Services.IServices;
using FluentResults;
using TODO.Core.Dto;

namespace TODO.Application.CQRS.Queries.SubTask.GetSubTaskById
{
    public class GetSubTaskByIdQueryHandler : IRequestHandler<GetSubTaskByIdQuery, SubTaskDTO>
    {
        private readonly ISubTaskRepositoryServices _subTaskService;

        public GetSubTaskByIdQueryHandler(ISubTaskRepositoryServices subTaskService)
        {
            _subTaskService = subTaskService;
        }

        public async Task<SubTaskDTO> Handle(GetSubTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _subTaskService.GetSubTaskById(request.Id);

            if (result.IsFailed)
                throw new ApplicationException(result.Errors[0].Message);

            return result.Value;
        }
    }
}
