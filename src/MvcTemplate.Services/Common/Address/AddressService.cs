using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Services
{
    public class AddressService : BaseService, IAddressService
    {
        public AddressService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<TView> Get<TView>(Int32 id) where TView : BaseSoftDeleteView
        {
            return await UnitOfWork.GetAsAsync<Address, TView>(id);
        }

        public IQueryable<AddressView> GetViews()
        {
            return UnitOfWork
                .Select<Address>()
                .To<AddressView>()
                .OrderByDescending(address => address.Id);
        }

        public async Task Create(AddressView view)
        {
            Address address = UnitOfWork.To<Address>(view);

            await UnitOfWork.InsertAsync(address);
            await UnitOfWork.SaveChangesAsync();
        }
        public async Task Edit(AddressView view)
        {
            Address address = UnitOfWork.To<Address>(view);

            UnitOfWork.Update(address);
            await UnitOfWork.SaveChangesAsync();
        }
        public async Task Delete(Int32 id)
        {
            UnitOfWork.Delete<Address>(id);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
