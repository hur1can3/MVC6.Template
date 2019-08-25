using System;
using System.ComponentModel.DataAnnotations;

namespace MvcTemplate.Objects
{
    public class Contact : BaseSoftDeleteModel
    {
        public virtual ContactType ContactType {get;set;}
    }
}
