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

namespace MvcTemplate.Controllers.Membership.Tests
{
    public class CustomerControllerTests : ControllerTests
    {
        private CustomerController controller;
        private ICustomerValidator validator;
        private ICustomerService service;
        private CustomerView customer;

        public CustomerControllerTests()
        {
            validator = Substitute.For<ICustomerValidator>();
            service = Substitute.For<ICustomerService>();

            customer = ObjectsFactory.CreateCustomerView();

            controller = Substitute.ForPartsOf<CustomerController>(validator, service);
            controller.ControllerContext.RouteData = new RouteData();
        }
        public override void Dispose()
        {
            controller.Dispose();
            validator.Dispose();
            service.Dispose();
        }

        [Fact]
        public void Index_ReturnsCustomerViews()
        {
            service.GetViews().Returns(Array.Empty<CustomerView>().AsQueryable());

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
            validator.CanCreate(customer).Returns(false);

            Object actual = Assert.IsType<ViewResult>(await controller.Create(customer)).Model;
            Object expected = customer;

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Create_Customer()
        {
            validator.CanCreate(customer).Returns(true);

            await controller.Create(customer);

            service.Received().Create(customer);
        }

        [Fact]
        public async Task Create_RedirectsToIndex()
        {
            validator.CanCreate(customer).Returns(true);

            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.Create(customer);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Details_ReturnsNotEmptyView()
        {
            await service.Get<CustomerView>(customer.Id).Returns(customer);

            Object expected = NotEmptyView(controller, customer);
            Object actual = await controller.Details(customer.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_ReturnsNotEmptyView()
        {
            await service.Get<CustomerView>(customer.Id).Returns(customer);

            Object expected = NotEmptyView(controller, customer);
            Object actual = await controller.Edit(customer.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_CanNotEdit_ReturnsSameView()
        {
            validator.CanEdit(customer).Returns(false);

            Object actual = Assert.IsType<ViewResult>(await controller.Edit(customer)).Model;
            Object expected = customer;

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Edit_Customer()
        {
            validator.CanEdit(customer).Returns(true);

            await controller.Edit(customer);

            service.Received().Edit(customer);
        }

        [Fact]
        public async Task Edit_RedirectsToIndex()
        {
            validator.CanEdit(customer).Returns(true);

            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.Edit(customer);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Delete_ReturnsNotEmptyView()
        {
            await service.Get<CustomerView>(customer.Id).Returns(customer);

            Object expected = NotEmptyView(controller, customer);
            Object actual = await controller.Delete(customer.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task DeleteConfirmed_DeletesCustomer()
        {
            await controller.DeleteConfirmed(customer.Id);

            service.Received().Delete(customer.Id);
        }

        [Fact]
        public async Task Delete_RedirectsToIndex()
        {
            Object expected = RedirectToAction(controller, "Index");
            Object actual = await controller.DeleteConfirmed(customer.Id);

            Assert.Same(expected, actual);
        }
    }
}
