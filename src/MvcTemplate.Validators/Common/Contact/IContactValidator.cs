using MvcTemplate.Objects;
using System;

namespace MvcTemplate.Validators
{
    public interface IContactValidator : IValidator
    {
        Boolean CanCreate(ContactView view);
        Boolean CanEdit(ContactView view);
    }
}
