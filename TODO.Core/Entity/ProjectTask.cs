using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TODO.Core.Entity
{
    public class ProjectTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime ExpiryDate { get; set; }

        public List<SubTask> SubTasks { get; set; } = new List<SubTask>();
        public bool IsActive => SubTasks.Any(s => s.IsActive && !s.IsCompleted);

        public int? TotalProgress { get; set; }

        public int GetTotalProgress()
        {

            if (SubTasks == null || SubTasks.Count(s => s.IsActive) == 0)
                return 0;

            return (int)(SubTasks.Count(s => s.IsActive && s.IsCompleted) / (double)SubTasks.Count(s => s.IsActive) * 100);
        }

    }
}
