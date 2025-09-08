using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TODO.Application.CQRS.Queries.GetAllProjectTasks;

namespace TODO.Application.Extension;

public static class ApplicationExtentions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetAllProjectTasksQueryHandle).Assembly);
            });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
