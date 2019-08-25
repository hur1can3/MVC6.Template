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
    public class ContactTypeControllerTests : ControllerTests
    {
        private ContactTypeController controller;
        private IContactTypeValidator validator;
        private IContactTypeService service;
        private ContactTypeView type;

        public ContactTypeControllerTests()
        {
            validator = Substitute.For<IContactTypeValidator>();
            service = Substitute.For<IContactTypeService>();

            type = ObjectsFactory.CreateContactTypeView();

            controller = Substitute.ForPartsOf<ContactTypeController>(validator, service);
            controller.ControllerContext.RouteData = new RouteData();
        }
        public override void Dispose()
        {
            controller.Dispose();
            validator.Dispose();
            service.Dispose();
        }

        [Fact]
        public void Index_ReturnsTypeViews()
        {
            service.GetViews().Returns(Array.Empty<ContactTypeView>().AsQueryable());

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
            validator.CanCreate(type).Returns(false);

            Object actual = Assert.IsType<ViewResult>(await controller.Create(type)).Model;
            Object expected = type;

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Create_Type()
        {
            validator.CanCreate(type).Returns(true);

            await controller.Create(type);

            service.Received().Create(type);
        }

        [Fact]
        public async Task Create_RedirectsToIndex()
        {
            validator.CanCreate(type).Returns(true);

            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.Create(type);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Details_ReturnsNotEmptyView()
        {
            await service.Get<ContactTypeView>(type.Id).Returns(type);

            Object expected = NotEmptyView(controller, type);
            Object actual = await controller.Details(type.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_ReturnsNotEmptyView()
        {
            await service.Get<ContactTypeView>(type.Id).Returns(type);

            Object expected = NotEmptyView(controller, type);
            Object actual = await controller.Edit(type.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_CanNotEdit_ReturnsSameView()
        {
            validator.CanEdit(type).Returns(false);

            Object actual = Assert.IsType<ViewResult>(await controller.Edit(type)).Model;
            Object expected = type;

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_Type()
        {
            validator.CanEdit(type).Returns(true);

            await controller.Edit(type);

            service.Received().Edit(type);
        }

        [Fact]
        public async Task Edit_RedirectsToIndex()
        {
            validator.CanEdit(type).Returns(true);

            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.Edit(type);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Delete_ReturnsNotEmptyView()
        {
            await service.Get<ContactTypeView>(type.Id).Returns(type);

            Object expected = NotEmptyView(controller, type);
            Object actual = await controller.Delete(type.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task DeleteConfirmed_DeletesType()
        {
            await controller.DeleteConfirmed(type.Id);

            service.Received().Delete(type.Id);
        }

        [Fact]
        public async Task Delete_RedirectsToIndex()
        {
            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.DeleteConfirmed(type.Id);

            Assert.Same(expected, actual);
        }
    }
}
