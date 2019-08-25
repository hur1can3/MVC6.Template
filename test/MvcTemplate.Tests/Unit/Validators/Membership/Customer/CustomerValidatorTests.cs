using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using MvcTemplate.Tests;
using System;
using Xunit;

namespace MvcTemplate.Validators.Tests
{
    public class CustomerValidatorTests : IDisposable
    {
        private CustomerValidator validator;
        private TestingContext context;
        private Customer customer;

        public CustomerValidatorTests()
        {
            context = new TestingContext();
            validator = new CustomerValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Customer>().Add(customer = ObjectsFactory.CreateCustomer());
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateCustomerView(1)));
        }

        [Fact]
        public void CanCreate_ValidCustomer()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateCustomerView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateCustomerView(customer.Id)));
        }

        [Fact]
        public void CanEdit_ValidCustomer()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateCustomerView(customer.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }
    }
}
