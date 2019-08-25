using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using MvcTemplate.Tests;
using System;
using Xunit;

namespace MvcTemplate.Validators.Tests
{
    public class AddressValidatorTests : IDisposable
    {
        private AddressValidator validator;
        private TestingContext context;
        private Address address;

        public AddressValidatorTests()
        {
            context = new TestingContext();
            validator = new AddressValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Address>().Add(address = ObjectsFactory.CreateAddress());
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

            Assert.False(validator.CanCreate(ObjectsFactory.CreateAddressView(1)));
        }

        [Fact]
        public void CanCreate_ValidAddress()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateAddressView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateAddressView(address.Id)));
        }

        [Fact]
        public void CanEdit_ValidAddress()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateAddressView(address.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }
    }
}
