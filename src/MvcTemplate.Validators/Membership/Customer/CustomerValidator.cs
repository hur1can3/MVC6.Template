using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using System;

namespace MvcTemplate.Validators
{
    public class CustomerValidator : BaseValidator, ICustomerValidator
    {
        public CustomerValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Boolean CanCreate(CustomerView view)
        {
            return ModelState.IsValid;
        }
        public Boolean CanEdit(CustomerView view)
        {
            return ModelState.IsValid;
        }
    }
}
