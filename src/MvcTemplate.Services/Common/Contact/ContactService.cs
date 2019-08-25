using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Services
{
    public class ContactService : BaseService, IContactService
    {
        public ContactService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<TView> Get<TView>(Int32 id) where TView : BaseSoftDeleteView
        {
            return await UnitOfWork.GetAsAsync<Contact, TView>(id);
        }

        public IQueryable<ContactView> GetViews()
        {
            return UnitOfWork
                .Select<Contact>()
                .To<ContactView>()
                .OrderByDescending(contact => contact.Id);
        }

        public async Task Create(ContactView view)
        {
            Contact contact = UnitOfWork.To<Contact>(view);

            await UnitOfWork.InsertAsync(contact);
            await UnitOfWork.SaveChangesAsync();
        }
        public async Task Edit(ContactView view)
        {
            Contact contact = UnitOfWork.To<Contact>(view);

            UnitOfWork.Update(contact);
            await UnitOfWork.SaveChangesAsync();
        }
        public async Task Delete(Int32 id)
        {
            UnitOfWork.Delete<Contact>(id);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
