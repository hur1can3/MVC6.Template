using System;

namespace MvcTemplate.Objects
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeletionDate { get; set; }
        int? DeletedByAccountId { get; set; }
        Account DeletedBy { get; set; }
    }
}
