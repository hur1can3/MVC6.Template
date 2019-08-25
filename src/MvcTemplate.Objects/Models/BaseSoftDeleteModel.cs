using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTemplate.Objects
{
    public abstract class BaseSoftDeleteModel : BaseModel, IDeletable
    {
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? DeletionDate { get; set; }
        public virtual int? DeletedByAccountId { get; set; }

        [ForeignKey(nameof(BaseSoftDeleteModel.DeletedByAccountId))]

        public virtual Account DeletedBy { get; set; }
    }
}