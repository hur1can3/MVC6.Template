using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTemplate.Objects
{
    public abstract class BaseObsoleteModel : BaseModel, IObsoletable
    {
        public virtual bool IObsoletable { get; set; }
        public virtual DateTime? ObsoletionDate { get; set; }
        public virtual int? ObsoletedByAccountId { get; set; }
        [ForeignKey(nameof(BaseObsoleteModel.ObsoletedByAccountId))]
        public virtual Account ObsoletedBy { get; set; }
    }
}
