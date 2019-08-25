using Microsoft.AspNetCore.Mvc;
using MvcTemplate.Components.Mvc;
using MvcTemplate.Objects;
using MvcTemplate.Services;
using MvcTemplate.Validators;
using System;
using System.Threading.Tasks;

namespace MvcTemplate.Controllers.Common
{
    [Area("Common")]
    public class AddressController : ValidatedController<IAddressValidator, IAddressService>
    {
        public AddressController(IAddressValidator validator, IAddressService service)
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
        public async Task<ActionResult> Create(AddressView address)
        {
            if (!Validator.CanCreate(address))
                return View(address);

            await Service.Create(address);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Details(Int32 id)
        {
            return NotEmptyView(await Service.Get<AddressView>(id));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Int32 id)
        {
            return NotEmptyView(await Service.Get<AddressView>(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AddressView address)
        {
            if (!Validator.CanEdit(address))
                return View(address);

            await Service.Edit(address);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Int32 id)
        {
            return NotEmptyView(await Service.Get<AddressView>(id));
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
