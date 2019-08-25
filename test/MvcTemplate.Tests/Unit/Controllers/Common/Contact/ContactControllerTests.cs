using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using MvcTemplate.Controllers.Tests;
using MvcTemplate.Objects;
using MvcTemplate.Services;
using MvcTemplate.Tests;
using MvcTemplate.Validators;
using NSubstitute;
using System;
using System.Linq;
using Xunit;
using System.Threading.Tasks;

namespace MvcTemplate.Controllers.Common.Tests
{
    public class ContactControllerTests : ControllerTests
    {
        private ContactController controller;
        private IContactValidator validator;
        private IContactService service;
        private ContactView contact;

        public ContactControllerTests()
        {
            validator = Substitute.For<IContactValidator>();
            service = Substitute.For<IContactService>();

            contact = ObjectsFactory.CreateContactView();

            controller = Substitute.ForPartsOf<ContactController>(validator, service);
            controller.ControllerContext.RouteData = new RouteData();
        }
        public override void Dispose()
        {
            controller.Dispose();
            validator.Dispose();
            service.Dispose();
        }

        [Fact]
        public void Index_ReturnsContactViews()
        {
            service.GetViews().Returns(Array.Empty<ContactView>().AsQueryable());

            Object actual = controller.Index().Model;
            Object expected = service.GetViews();

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Create_ReturnsEmptyView()
        {
            ViewResult actual = await controller.Create();

            Assert.Null(actual.Model);
        }

        [Fact]
        public async Task Create_CanNotCreate_ReturnsSameView()
        {
            validator.CanCreate(contact).Returns(false);

            Object actual = Assert.IsType<ViewResult>(await controller.Create(contact)).Model;
            Object expected = contact;

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Create_Contact()
        {
            validator.CanCreate(contact).Returns(true);

            await controller.Create(contact);

            service.Received().Create(contact);
        }

        [Fact]
        public async Task Create_RedirectsToIndex()
        {
            validator.CanCreate(contact).Returns(true);

            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.Create(contact);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Details_ReturnsNotEmptyView()
        {
            await service.Get<ContactView>(contact.Id).Returns(contact);

            Object expected = NotEmptyView(controller, contact);
            Object actual = await controller.Details(contact.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_ReturnsNotEmptyView()
        {
            await service.Get<ContactView>(contact.Id).Returns(contact);

            Object expected = NotEmptyView(controller, contact);
            Object actual = await controller.Edit(contact.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_CanNotEdit_ReturnsSameView()
        {
            validator.CanEdit(contact).Returns(false);

            Object actual = Assert.IsType<ViewResult>(await controller.Edit(contact)).Model;
            Object expected = contact;

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_Contact()
        {
            validator.CanEdit(contact).Returns(true);

            await controller.Edit(contact);

            service.Received().Edit(contact);
        }

        [Fact]
        public async Task Edit_RedirectsToIndex()
        {
            validator.CanEdit(contact).Returns(true);

            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.Edit(contact);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Delete_ReturnsNotEmptyView()
        {
            await service.Get<ContactView>(contact.Id).Returns(contact);

            Object expected = NotEmptyView(controller, contact);
            Object actual = await controller.Delete(contact.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task DeleteConfirmed_DeletesContact()
        {
            await controller.DeleteConfirmed(contact.Id);

            service.Received().Delete(contact.Id);
        }

        [Fact]
        public async Task Delete_RedirectsToIndex()
        {
            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.DeleteConfirmed(contact.Id);

            Assert.Same(expected, actual);
        }
    }
}
