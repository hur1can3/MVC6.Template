using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseActiveSoftDeleteView : BaseSoftDeleteView, IActivatable
    {
        public virtual bool IsActive { get; set; }
        public virtual DateTime? ActivationDate { get; set; }

        public virtual int? ActivatedByAccountId {get;set;}
        public virtual Account ActivatedBy { get; set; }
    }
}