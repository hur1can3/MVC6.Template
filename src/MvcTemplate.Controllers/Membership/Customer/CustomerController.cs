using Microsoft.AspNetCore.Mvc;
using MvcTemplate.Components.Mvc;
using MvcTemplate.Objects;
using MvcTemplate.Services;
using MvcTemplate.Validators;
using System;
using System.Threading.Tasks;

namespace MvcTemplate.Controllers.Membership
{
    [Area("Membership")]
    public class CustomerController : ValidatedController<ICustomerValidator, ICustomerService>
    {
        public CustomerController(ICustomerValidator validator, ICustomerService service)
            : base(validator, service)
        {
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(Service.GetViews());
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CustomerView customer)
        {
            if (!Validator.CanCreate(customer))
                return View(customer);

            await Service.Create(customer);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Details(Int32 id)
        {
            return NotEmptyView(await Service.Get<CustomerView>(id));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Int32 id)
        {
            return NotEmptyView(await Service.Get<CustomerView>(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CustomerView customer)
        {
            if (!Validator.CanEdit(customer))
                return View(customer);

            await Service.Edit(customer);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Int32 id)
        {
            return NotEmptyView(await Service.Get<CustomerView>(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<RedirectToActionResult> DeleteConfirmed(Int32 id)
        {
            await Service.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
