using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using System;

namespace MvcTemplate.Validators
{
    public class AddressValidator : BaseValidator, IAddressValidator
    {
        public AddressValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Boolean CanCreate(AddressView view)
        {
            return ModelState.IsValid;
        }
        public Boolean CanEdit(AddressView view)
        {
            return ModelState.IsValid;
        }
    }
}
