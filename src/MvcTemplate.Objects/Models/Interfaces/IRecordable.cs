using System;

namespace MvcTemplate.Objects
{
    public interface IRecordable
    {
        DateTime? RecordActiveStartDate { get; set; }
        DateTime? RecordActiveEndDate { get; set; }
    }
}
