using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Services
{
    public class AddressTypeService : BaseService, IAddressTypeService
    {
        public AddressTypeService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<TView> Get<TView>(Int32 id) where TView : BaseCodeView
        {
            return await UnitOfWork.GetAsAsync<AddressType, TView>(id);
        }

        public IQueryable<AddressTypeView> GetViews()
        {
            return UnitOfWork
                .Select<AddressType>()
                .To<AddressTypeView>()
                .OrderByDescending(type => type.Id);
        }

        public async Task Create(AddressTypeView view)
        {
            AddressType type = UnitOfWork.To<AddressType>(view);

            await UnitOfWork.InsertAsync(type);
            await UnitOfWork.SaveChangesAsync();
        }
        public async Task Edit(AddressTypeView view)
        {
            AddressType type = UnitOfWork.To<AddressType>(view);

            UnitOfWork.Update(type);
            await UnitOfWork.SaveChangesAsync();
        }
        public async Task Delete(Int32 id)
        {
            UnitOfWork.Delete<AddressType>(id);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
