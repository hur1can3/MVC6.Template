using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcTemplate.Objects
{
    public class Customer : BaseActiveSoftDeleteModel
    {
        public long Number { get; set; }
        public long Name { get; set; }

        public virtual ICollection<Address> Addresses {get;set;}
        public virtual ICollection<Contact> Contacts {get;set;}
    }
}
