using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseRecordModel : BaseModel, IRecordable
    {
        public virtual DateTime? RecordActiveStartDate { get; set; }
        public virtual DateTime? RecordActiveEndDate { get; set; }
    }
}
