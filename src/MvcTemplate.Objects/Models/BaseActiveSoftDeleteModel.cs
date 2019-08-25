using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTemplate.Objects
{
    public abstract class BaseActiveSoftDeleteModel : BaseSoftDeleteModel, IActivatable
    {
        public virtual bool IsActive { get; set; }
        public virtual DateTime? ActivationDate { get; set; }
        [ForeignKey(nameof(BaseActiveSoftDeleteModel.ActivatedByAccountId))]
        public virtual int? ActivatedByAccountId { get; set; }


        public virtual Account ActivatedBy { get; set; }
    }
}
