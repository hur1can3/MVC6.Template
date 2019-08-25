using System;

namespace MvcTemplate.Objects
{
    public interface IActivatable
    {
        bool IsActive { get; set; }
        DateTime? ActivationDate { get; set; }
        int? ActivatedByAccountId { get; set; }
        Account ActivatedBy { get; set; }
    }
}