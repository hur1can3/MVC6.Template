using MvcTemplate.Objects;
using System;

namespace MvcTemplate.Validators
{
    public interface IAddressValidator : IValidator
    {
        Boolean CanCreate(AddressView view);
        Boolean CanEdit(AddressView view);
    }
}
