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
    public class ContactController : ValidatedController<IContactValidator, IContactService>
    {
        public ContactController(IContactValidator validator, IContactService service)
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
        public async Task<ActionResult> Create(ContactView contact)
        {
            if (!Validator.CanCreate(contact))
                return View(contact);

            await Service.Create(contact);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Details(Int32 id)
        {
            return NotEmptyView(await Service.Get<ContactView>(id));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Int32 id)
        {
            return NotEmptyView(await Service.Get<ContactView>(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ContactView contact)
        {
            if (!Validator.CanEdit(contact))
                return View(contact);

            await Service.Edit(contact);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Int32 id)
        {
            return NotEmptyView(await Service.Get<ContactView>(id));
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
