using Microsoft.EntityFrameworkCore;
using System;

namespace MPED.Domain
{
    public abstract class BaseEntity : DbContext
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
