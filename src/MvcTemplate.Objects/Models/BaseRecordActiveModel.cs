using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseRecordActiveModel : BaseActiveModel, IRecordable
    {
        public virtual DateTime? RecordActiveStartDate { get; set; }
        public virtual DateTime? RecordActiveEndDate { get; set; }
    }
}
