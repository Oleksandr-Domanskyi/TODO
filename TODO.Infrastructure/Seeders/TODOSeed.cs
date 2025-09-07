using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO.Core.Entity;
using TODO.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace TODO.Infrastructure.Seeders;

public class TODOSeed
{
    private readonly TodoDbContext _dbContext;

    public TODOSeed(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (await _dbContext.ProjectTasks.AnyAsync())
            return;

        var projects = new List<ProjectTask>
        {
            new ProjectTask
            {
                Title = "Website Redesign",
                Description = "Complete redesign of company website to improve UX and performance",
                ExpiryDate = DateTime.UtcNow.AddDays(60),
                SubTasks = new List<SubTask>
                {
                    new SubTask
                    {
                        Title = "Design new homepage",
                        Description = "Create wireframes and final design for homepage",
                        ExpiryDate = DateTime.UtcNow.AddDays(10),
                        IsActive = true,
                        IsCompleted = false
                    },
                    new SubTask
                    {
                        Title = "Implement responsive layout",
                        Description = "Ensure website works well on mobile, tablet, and desktop",
                        ExpiryDate = DateTime.UtcNow.AddDays(20),
                        IsActive = true,
                        IsCompleted = true
                    },
                    new SubTask
                    {
                        Title = "Test cross-browser compatibility",
                        Description = "Check website on Chrome, Firefox, Safari, and Edge",
                        ExpiryDate = DateTime.UtcNow.AddDays(25),
                        IsActive = false,
                        IsCompleted = false
                    }
                }
            },
            new ProjectTask
            {
                Title = "Marketing Campaign",
                Description = "Launch social media campaign to promote new product line",
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                SubTasks = new List<SubTask>
                {
                    new SubTask
                    {
                        Title = "Create campaign graphics",
                        Description = "Design banners, posts, and stories for Instagram and Facebook",
                        ExpiryDate = DateTime.UtcNow.AddDays(5),
                        IsActive = true,
                        IsCompleted = true
                    },
                    new SubTask
                    {
                        Title = "Schedule posts",
                        Description = "Plan content calendar and schedule posts using Buffer",
                        ExpiryDate = DateTime.UtcNow.AddDays(10),
                        IsActive = true,
                        IsCompleted = false
                    },
                    new SubTask
                    {
                        Title = "Analyze engagement metrics",
                        Description = "Track likes, shares, comments, and reach",
                        ExpiryDate = DateTime.UtcNow.AddDays(15),
                        IsActive = true,
                        IsCompleted = false
                    }
                }
            },
            new ProjectTask
            {
                Title = "Team Training",
                Description = "Organize internal workshops for skill improvement",
                ExpiryDate = DateTime.UtcNow.AddDays(45),
                SubTasks = new List<SubTask>
                {
                    new SubTask
                    {
                        Title = "Schedule workshop sessions",
                        Description = "Find suitable dates and rooms for all sessions",
                        ExpiryDate = DateTime.UtcNow.AddDays(7),
                        IsActive = true,
                        IsCompleted = false
                    },
                    new SubTask
                    {
                        Title = "Prepare training materials",
                        Description = "Create presentations, handouts, and exercises",
                        ExpiryDate = DateTime.UtcNow.AddDays(12),
                        IsActive = true,
                        IsCompleted = true
                    },
                    new SubTask
                    {
                        Title = "Collect feedback",
                        Description = "Send surveys and analyze responses after training",
                        ExpiryDate = DateTime.UtcNow.AddDays(20),
                        IsActive = false,
                        IsCompleted = false
                    }
                }
            }
        };

        foreach (var project in projects)
        {
            var activeSubTasks = project.SubTasks.Where(s => s.IsActive).ToList();
            project.TotalProgress = activeSubTasks.Count == 0
                ? 0
                : (int)(activeSubTasks.Count(s => s.IsCompleted) / (double)activeSubTasks.Count * 100);
        }

        await _dbContext.ProjectTasks.AddRangeAsync(projects);
        await _dbContext.SaveChangesAsync();
    }
}
