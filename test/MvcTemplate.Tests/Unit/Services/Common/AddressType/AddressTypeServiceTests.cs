using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using MvcTemplate.Tests;
using System;
using System.Linq;
using Xunit;

namespace MvcTemplate.Services.Tests
{
    public class AddressTypeServiceTests : IDisposable
    {
        private AddressTypeService service;
        private TestingContext context;
        private AddressType type;

        public AddressTypeServiceTests()
        {
            context = new TestingContext();
            service = new AddressTypeService(new UnitOfWork(new TestingContext(context)));

            context.Set<AddressType>().Add(type = ObjectsFactory.CreateAddressType());
            context.SaveChanges();
        }
        public void Dispose()
        {
            service.Dispose();
            context.Dispose();
        }

        [Fact]
        public void Get_ReturnsViewById()
        {
            AddressTypeView actual = service.Get<AddressTypeView>(type.Id);
            AddressTypeView expected = Mapper.Map<AddressTypeView>(type);

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void GetViews_ReturnsTypeViews()
        {
            AddressTypeView[] actual = service.GetViews().ToArray();
            AddressTypeView[] expected = context
                .Set<AddressType>()
                .ProjectTo<AddressTypeView>()
                .OrderByDescending(view => view.CreationDate)
                .ToArray();

            for (Int32 i = 0; i < expected.Length || i < actual.Length; i++)
            {
                Assert.Equal(expected[i].ModifiedByAccountId, actual[i].ModifiedByAccountId);
                Assert.Equal(expected[i].CreatedByAccountId, actual[i].CreatedByAccountId);
                Assert.Equal(expected[i].ModificationDate, actual[i].ModificationDate);
                Assert.Equal(expected[i].CreationDate, actual[i].CreationDate);
                Assert.Equal(expected[i].ModifiedBy, actual[i].ModifiedBy);
                Assert.Equal(expected[i].CreatedBy, actual[i].CreatedBy);
                Assert.Equal(expected[i].Id, actual[i].Id);
            }
            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void Create_Type()
        {
            AddressTypeView view = ObjectsFactory.CreateAddressTypeView(1);

            service.Create(view);

            AddressType actual = context.Set<AddressType>().AsNoTracking().Single(model => model.Id != type.Id);
            AddressTypeView expected = view;

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void Edit_Type()
        {
            AddressTypeView view = ObjectsFactory.CreateAddressTypeView(type.Id);

            Assert.True(false, "Above properties were not sanity checked");

            service.Edit(view);

            AddressType actual = context.Set<AddressType>().AsNoTracking().Single();
            AddressType expected = type;

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void Delete_Type()
        {
            service.Delete(type.Id);

            Assert.Empty(context.Set<AddressType>());
        }
    }
}
