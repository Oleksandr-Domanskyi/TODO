using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TODO.Infrastructur.Repositories;
using TODO.Infrastructure.DataBase;
using TODO.Infrastructure.Repositories;
using TODO.Infrastructure.Repositories.IRepositories;
using TODO.Infrastructure.Seeders;
using TODO.Infrastructure.Services;
using TODO.Infrastructure.Services.IServices;
namespace TODO.Infrastructure.Extension;

public static class TodoInfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuretion)
    {
        services.AddDbContext<TodoDbContext>(options =>
        {
            options.UseSqlServer(configuretion.GetConnectionString("SqlConnectionString"));
        });

        services.AddTransient<TODOSeed>();
        services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
        services.AddScoped<IProjectTaskRepositoryServices, ProjectTaskRepositoryServices>();
        services.AddScoped<ISubTaskRepository, SubTaskRepository>();
        services.AddScoped<ISubTaskRepositoryServices, SubTaskRepositoryServices>();
    }

    public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<TODOSeed>();
        await seeder.SeedAsync();
    }
}
