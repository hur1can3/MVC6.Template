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
    public class ContactServiceTests : IDisposable
    {
        private ContactService service;
        private TestingContext context;
        private Contact contact;

        public ContactServiceTests()
        {
            context = new TestingContext();
            service = new ContactService(new UnitOfWork(new TestingContext(context)));

            context.Set<Contact>().Add(contact = ObjectsFactory.CreateContact());
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
            ContactView actual = service.Get<ContactView>(contact.Id);
            ContactView expected = Mapper.Map<ContactView>(contact);

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void GetViews_ReturnsContactViews()
        {
            ContactView[] actual = service.GetViews().ToArray();
            ContactView[] expected = context
                .Set<Contact>()
                .ProjectTo<ContactView>()
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
        public void Create_Contact()
        {
            ContactView view = ObjectsFactory.CreateContactView(1);

            service.Create(view);

            Contact actual = context.Set<Contact>().AsNoTracking().Single(model => model.Id != contact.Id);
            ContactView expected = view;

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void Edit_Contact()
        {
            ContactView view = ObjectsFactory.CreateContactView(contact.Id);

            Assert.True(false, "Above properties were not sanity checked");

            service.Edit(view);

            Contact actual = context.Set<Contact>().AsNoTracking().Single();
            Contact expected = contact;

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void Delete_Contact()
        {
            service.Delete(contact.Id);

            Assert.Empty(context.Set<Contact>());
        }
    }
}
