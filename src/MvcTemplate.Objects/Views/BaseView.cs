using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseView : ICreatable, IModifiable
    {
        public virtual Int32 Id
        {
            get;
            set;
        }

        public virtual DateTime? ModificationDate { get; set; }

        public virtual int? CreatedByAccountId { get; set; }
        public virtual Account CreatedBy { get; set; }


        public virtual int? ModifiedByAccountId { get; set; }
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
