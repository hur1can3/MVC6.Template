using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using System;

namespace MvcTemplate.Validators
{
    public class ContactValidator : BaseValidator, IContactValidator
    {
        public ContactValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Boolean CanCreate(ContactView view)
        {
            return ModelState.IsValid;
        }
        public Boolean CanEdit(ContactView view)
        {
            return ModelState.IsValid;
        }
    }
}
