using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseActiveView : BaseView, IActivatable
    {
        public virtual bool IsActive { get; set; }
        public virtual DateTime? ActivationDate { get; set; }

        public virtual int? ActivatedByAccountId { get; set; }
        public virtual Account ActivatedBy { get; set; }
    }
}
