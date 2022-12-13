using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        [AllowNull]
        public string? UpdatedBy { get; set; }
        [AllowNull]
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
