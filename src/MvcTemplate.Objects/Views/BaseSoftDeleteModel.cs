using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseSoftDeleteView : BaseView, IDeletable
    {
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? DeletionDate { get; set; }
        public virtual int? DeletedByAccountId { get; set; }
        public virtual Account DeletedBy { get; set; }
    }
}
