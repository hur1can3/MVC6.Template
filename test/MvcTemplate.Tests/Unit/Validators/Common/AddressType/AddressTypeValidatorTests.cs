using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using MvcTemplate.Tests;
using System;
using Xunit;

namespace MvcTemplate.Validators.Tests
{
    public class AddressTypeValidatorTests : IDisposable
    {
        private AddressTypeValidator validator;
        private TestingContext context;
        private AddressType type;

        public AddressTypeValidatorTests()
        {
            context = new TestingContext();
            validator = new AddressTypeValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<AddressType>().Add(type = ObjectsFactory.CreateAddressType());
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

            Assert.False(validator.CanCreate(ObjectsFactory.CreateAddressTypeView(1)));
        }

        [Fact]
        public void CanCreate_ValidType()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateAddressTypeView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateAddressTypeView(type.Id)));
        }

        [Fact]
        public void CanEdit_ValidType()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateAddressTypeView(type.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }
    }
}
