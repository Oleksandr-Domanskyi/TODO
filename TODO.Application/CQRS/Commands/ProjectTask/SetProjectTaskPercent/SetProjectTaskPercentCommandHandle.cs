using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Application.CQRS.Commands.SetProjectTaskPercent
{
    public class SetAsComplateProjectTaskPercentCommandHandle : IRequestHandler<SetAsComplateProjectTaskPercentCommand, Unit>
    {
        private readonly IProjectTaskRepositoryServices _projectTaskRepositoryServices;

        public SetAsComplateProjectTaskPercentCommandHandle(IProjectTaskRepositoryServices projectTaskRepositoryServices)
        {
            _projectTaskRepositoryServices = projectTaskRepositoryServices;
        }

        public async Task<Unit> Handle(SetAsComplateProjectTaskPercentCommand request, CancellationToken cancellationToken)
        {
            var model = await _projectTaskRepositoryServices.SetAsComplatePercentComplete(request.Id);

            if (!model.IsSuccess)
                throw new Exception("Set as complate project task has error");

            return Unit.Value;
        }
    }
}
