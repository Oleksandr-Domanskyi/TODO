using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO.Core.Dto
{
    public class SubTaskDTO
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
    }
}