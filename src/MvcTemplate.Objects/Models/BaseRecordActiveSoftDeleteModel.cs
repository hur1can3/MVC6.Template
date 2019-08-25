
using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseRecordActiveSoftDeleteModel : BaseActiveSoftDeleteModel, IRecordable
    {
        public virtual DateTime? RecordActiveStartDate { get; set; }
        public virtual DateTime? RecordActiveEndDate { get; set; }
    }
}