using System;
using System.ComponentModel.DataAnnotations;

namespace TODO.Core.Entity
{
    public class SubTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsCompleted { get; set; } = false;


        public Guid ProjectTask_Id { get; set; } = default!;
    }
}
