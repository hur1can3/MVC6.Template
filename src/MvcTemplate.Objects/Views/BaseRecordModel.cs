using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseRecordView : BaseView, IRecordable
    {
        public virtual DateTime? RecordActiveStartDate { get; set; }
        public virtual DateTime? RecordActiveEndDate { get; set; }
    }
}
