using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using System;

namespace MvcTemplate.Validators
{
    public class AddressTypeValidator : BaseValidator, IAddressTypeValidator
    {
        public AddressTypeValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Boolean CanCreate(AddressTypeView view)
        {
            return ModelState.IsValid;
        }
        public Boolean CanEdit(AddressTypeView view)
        {
            return ModelState.IsValid;
        }
    }
}
