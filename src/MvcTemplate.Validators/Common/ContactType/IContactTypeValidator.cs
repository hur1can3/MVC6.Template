using MvcTemplate.Objects;
using System;

namespace MvcTemplate.Validators
{
    public interface IContactTypeValidator : IValidator
    {
        Boolean CanCreate(ContactTypeView view);
        Boolean CanEdit(ContactTypeView view);
    }
}
