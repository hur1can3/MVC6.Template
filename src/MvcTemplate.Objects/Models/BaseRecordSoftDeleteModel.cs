using System;


namespace MvcTemplate.Objects
{
    public abstract class BaseRecordSoftDeleteModel : BaseSoftDeleteModel, IRecordable
    {
        public virtual DateTime? RecordActiveStartDate { get; set; }
        public virtual DateTime? RecordActiveEndDate { get; set; }
    }
}