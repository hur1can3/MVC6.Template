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
    public class AddressControllerTests : ControllerTests
    {
        private AddressController controller;
        private IAddressValidator validator;
        private IAddressService service;
        private AddressView address;

        public AddressControllerTests()
        {
            validator = Substitute.For<IAddressValidator>();
            service = Substitute.For<IAddressService>();

            address = ObjectsFactory.CreateAddressView();

            controller = Substitute.ForPartsOf<AddressController>(validator, service);
            controller.ControllerContext.RouteData = new RouteData();
        }
        public override void Dispose()
        {
            controller.Dispose();
            validator.Dispose();
            service.Dispose();
        }

        [Fact]
        public void Index_ReturnsAddressViews()
        {
            service.GetViews().Returns(Array.Empty<AddressView>().AsQueryable());

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
            validator.CanCreate(address).Returns(false);

            Object actual = Assert.IsType<ViewResult>(await controller.Create(address)).Model;
            Object expected = address;

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Create_Address()
        {
            validator.CanCreate(address).Returns(true);

            await controller.Create(address);

            service.Received().Create(address);
        }

        [Fact]
        public async Task Create_RedirectsToIndex()
        {
            validator.CanCreate(address).Returns(true);

            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.Create(address);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Details_ReturnsNotEmptyView()
        {
            await service.Get<AddressView>(address.Id).Returns(address);

            Object expected = NotEmptyView(controller, address);
            Object actual = await controller.Details(address.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_ReturnsNotEmptyView()
        {
            await service.Get<AddressView>(address.Id).Returns(address);

            Object expected = NotEmptyView(controller, address);
            Object actual = await controller.Edit(address.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_CanNotEdit_ReturnsSameView()
        {
            validator.CanEdit(address).Returns(false);

            Object actual = Assert.IsType<ViewResult>(await controller.Edit(address)).Model;
            Object expected = address;

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_Address()
        {
            validator.CanEdit(address).Returns(true);

            await controller.Edit(address);

            service.Received().Edit(address);
        }

        [Fact]
        public async Task Edit_RedirectsToIndex()
        {
            validator.CanEdit(address).Returns(true);

            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.Edit(address);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Delete_ReturnsNotEmptyView()
        {
            await service.Get<AddressView>(address.Id).Returns(address);

            Object expected = NotEmptyView(controller, address);
            Object actual = await controller.Delete(address.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task DeleteConfirmed_DeletesAddress()
        {
            await controller.DeleteConfirmed(address.Id);

            service.Received().Delete(address.Id);
        }

        [Fact]
        public async Task Delete_RedirectsToIndex()
        {
            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.DeleteConfirmed(address.Id);

            Assert.Same(expected, actual);
        }
    }
}
