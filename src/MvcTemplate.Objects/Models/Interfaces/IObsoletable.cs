using System;

namespace MvcTemplate.Objects
{
    public interface IObsoletable
    {

        bool IObsoletable { get; set; }
        DateTime? ObsoletionDate { get; set; }
        int? ObsoletedByAccountId { get; set; }
        Account ObsoletedBy { get; set; }
    }
}