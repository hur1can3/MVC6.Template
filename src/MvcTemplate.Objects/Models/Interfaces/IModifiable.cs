using System;

namespace MvcTemplate.Objects
{
    public interface IModifiable
    {
        DateTime? ModificationDate { get; set; }
        int? ModifiedByAccountId { get; set; }
        Account ModifiedBy { get; set; }
    }
}