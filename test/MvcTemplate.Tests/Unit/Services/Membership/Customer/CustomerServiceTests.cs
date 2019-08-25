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
    public class CustomerServiceTests : IDisposable
    {
        private CustomerService service;
        private TestingContext context;
        private Customer customer;

        public CustomerServiceTests()
        {
            context = new TestingContext();
            service = new CustomerService(new UnitOfWork(new TestingContext(context)));

            context.Set<Customer>().Add(customer = ObjectsFactory.CreateCustomer());
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
            CustomerView actual = service.Get<CustomerView>(customer.Id);
            CustomerView expected = Mapper.Map<CustomerView>(customer);

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void GetViews_ReturnsCustomerViews()
        {
            CustomerView[] actual = service.GetViews().ToArray();
            CustomerView[] expected = context
                .Set<Customer>()
                .ProjectTo<CustomerView>()
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
        public void Create_Customer()
        {
            CustomerView view = ObjectsFactory.CreateCustomerView(1);

            service.Create(view);

            Customer actual = context.Set<Customer>().AsNoTracking().Single(model => model.Id != customer.Id);
            CustomerView expected = view;

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void Edit_Customer()
        {
            CustomerView view = ObjectsFactory.CreateCustomerView(customer.Id);

            Assert.True(false, "Above properties were not sanity checked");

            service.Edit(view);

            Customer actual = context.Set<Customer>().AsNoTracking().Single();
            Customer expected = customer;

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void Delete_Customer()
        {
            service.Delete(customer.Id);

            Assert.Empty(context.Set<Customer>());
        }
    }
}
