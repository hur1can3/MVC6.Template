using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTemplate.Objects
{
    public abstract class BaseModel : ICreatable, IModifiable
    {
        [Key]
        public virtual Int32 Id
        {
            get;
            set;
        }
        public virtual DateTime? ModificationDate { get; set; }

        public virtual int? CreatedByAccountId { get; set; }

        [ForeignKey(nameof(BaseModel.CreatedByAccountId))]

        public virtual Account CreatedBy { get; set; }


        public virtual int? ModifiedByAccountId { get; set; }

        [ForeignKey(nameof(BaseModel.ModifiedByAccountId))]
        public virtual Account ModifiedBy { get; set; }

        public virtual DateTime CreationDate
        {
            get
            {
                if (!IsCreationDateSet)
                    CreationDate = DateTime.Now;

                return InternalCreationDate;
            }
            protected set
            {
                IsCreationDateSet = true;
                InternalCreationDate = value;
            }
        }
        private Boolean IsCreationDateSet
        {
            get;
            set;
        }
        private DateTime InternalCreationDate
        {
            get;
            set;
        }
    }
}
