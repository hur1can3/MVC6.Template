using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseRecordObsoleteModel : BaseObsoleteModel, IRecordable
    {
        public virtual DateTime? RecordActiveStartDate { get; set; }
        public virtual DateTime? RecordActiveEndDate { get; set; }
    }
}