using System;
using System.ComponentModel.DataAnnotations;

namespace MvcTemplate.Objects
{
    public class Address : BaseSoftDeleteModel
    {
        public virtual AddressType AddressType {get;set;}
    }
}
