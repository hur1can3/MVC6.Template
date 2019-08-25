using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseObsoleteSoftDeleteView : BaseSoftDeleteView, IObsoletable
    {
        public virtual bool IObsoletable { get; set; }
        public virtual DateTime? ObsoletionDate { get; set; }
        public virtual int? ObsoletedByAccountId { get; set; }
        public virtual Account ObsoletedBy { get; set; }
    }
}