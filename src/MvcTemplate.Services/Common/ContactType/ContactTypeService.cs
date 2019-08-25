using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Services
{
    public class ContactTypeService : BaseService, IContactTypeService
    {
        public ContactTypeService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<TView> Get<TView>(Int32 id) where TView : BaseCodeView
        {
            return await UnitOfWork.GetAsAsync<ContactType, TView>(id);
        }

        public IQueryable<ContactTypeView> GetViews()
        {
            return UnitOfWork
                .Select<ContactType>()
                .To<ContactTypeView>()
                .OrderByDescending(type => type.Id);
        }

        public async Task Create(ContactTypeView view)
        {
            ContactType type = UnitOfWork.To<ContactType>(view);

            await UnitOfWork.InsertAsync(type);
            await UnitOfWork.SaveChangesAsync();
        }
        public async Task Edit(ContactTypeView view)
        {
            ContactType type = UnitOfWork.To<ContactType>(view);

            UnitOfWork.Update(type);
            await UnitOfWork.SaveChangesAsync();
        }
        public async Task Delete(Int32 id)
        {
            UnitOfWork.Delete<ContactType>(id);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
