using MvcTemplate.Objects;
using System;

namespace MvcTemplate.Validators
{
    public interface ICustomerValidator : IValidator
    {
        Boolean CanCreate(CustomerView view);
        Boolean CanEdit(CustomerView view);
    }
}
