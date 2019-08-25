using System;

namespace MvcTemplate.Objects
{
    public interface ICreatable
    {
        DateTime CreationDate { get; }
        int? CreatedByAccountId { get; set; }
        Account CreatedBy { get; set; }
    }
}
