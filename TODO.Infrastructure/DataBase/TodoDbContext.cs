using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TODO.Core.Entity;

namespace TODO.Infrastructure.DataBase;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {

    }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<SubTask> TodoModSubTaskModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
