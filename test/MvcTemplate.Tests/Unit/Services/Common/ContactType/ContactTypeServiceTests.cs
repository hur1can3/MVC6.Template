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
    public class ContactTypeServiceTests : IDisposable
    {
        private ContactTypeService service;
        private TestingContext context;
        private ContactType type;

        public ContactTypeServiceTests()
        {
            context = new TestingContext();
            service = new ContactTypeService(new UnitOfWork(new TestingContext(context)));

            context.Set<ContactType>().Add(type = ObjectsFactory.CreateContactType());
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
            ContactTypeView actual = service.Get<ContactTypeView>(type.Id);
            ContactTypeView expected = Mapper.Map<ContactTypeView>(type);

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void GetViews_ReturnsTypeViews()
        {
            ContactTypeView[] actual = service.GetViews().ToArray();
            ContactTypeView[] expected = context
                .Set<ContactType>()
                .ProjectTo<ContactTypeView>()
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
            ContactTypeView view = ObjectsFactory.CreateContactTypeView(1);

            service.Create(view);

            ContactType actual = context.Set<ContactType>().AsNoTracking().Single(model => model.Id != type.Id);
            ContactTypeView expected = view;

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void Edit_Type()
        {
            ContactTypeView view = ObjectsFactory.CreateContactTypeView(type.Id);

            Assert.True(false, "Above properties were not sanity checked");

            service.Edit(view);

            ContactType actual = context.Set<ContactType>().AsNoTracking().Single();
            ContactType expected = type;

            Assert.True(false, "Not all properties tested");
        }

        [Fact]
        public void Delete_Type()
        {
            service.Delete(type.Id);

            Assert.Empty(context.Set<ContactType>());
        }
    }
}
