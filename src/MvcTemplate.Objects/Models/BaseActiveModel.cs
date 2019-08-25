using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTemplate.Objects
{
    public abstract class BaseActiveModel : BaseModel, IActivatable
    {
        public virtual bool IsActive { get; set; }
        public virtual DateTime? ActivationDate { get; set; }

        public virtual int? ActivatedByAccountId { get; set; }
        [ForeignKey(nameof(BaseActiveModel.ActivatedByAccountId))]

        public virtual Account ActivatedBy { get; set; }
    }
}