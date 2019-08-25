using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using System;

namespace MvcTemplate.Validators
{
    public class ContactTypeValidator : BaseValidator, IContactTypeValidator
    {
        public ContactTypeValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Boolean CanCreate(ContactTypeView view)
        {
            return ModelState.IsValid;
        }
        public Boolean CanEdit(ContactTypeView view)
        {
            return ModelState.IsValid;
        }
    }
}
