using MvcTemplate.Objects;
using System;

namespace MvcTemplate.Validators
{
    public interface IAddressTypeValidator : IValidator
    {
        Boolean CanCreate(AddressTypeView view);
        Boolean CanEdit(AddressTypeView view);
    }
}
