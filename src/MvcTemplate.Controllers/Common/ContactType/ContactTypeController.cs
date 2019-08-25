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
    public class ContactTypeController : ValidatedController<IContactTypeValidator, IContactTypeService>
    {
        public ContactTypeController(IContactTypeValidator validator, IContactTypeService service)
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
        public async Task<ActionResult> Create(ContactTypeView type)
        {
            if (!Validator.CanCreate(type))
                return View(type);

            await Service.Create(type);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Details(Int32 id)
        {
            return NotEmptyView(await Service.Get<ContactTypeView>(id));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Int32 id)
        {
            return NotEmptyView(await Service.Get<ContactTypeView>(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ContactTypeView type)
        {
            if (!Validator.CanEdit(type))
                return View(type);

            await Service.Edit(type);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Int32 id)
        {
            return NotEmptyView(await Service.Get<ContactTypeView>(id));
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
