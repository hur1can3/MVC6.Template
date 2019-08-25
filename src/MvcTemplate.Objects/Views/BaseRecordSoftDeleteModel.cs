using System;


namespace MvcTemplate.Objects
{
    public abstract class BaseRecordSoftDeleteView : BaseSoftDeleteView, IRecordable
    {
        public virtual DateTime? RecordActiveStartDate { get; set; }
        public virtual DateTime? RecordActiveEndDate { get; set; }
    }
}
