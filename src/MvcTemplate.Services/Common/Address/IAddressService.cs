using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Services
{
    public interface IAddressService : IService
    {
        Task<TView> Get<TView>(Int32 id) where TView : BaseSoftDeleteView;
        IQueryable<AddressView> GetViews();

        Task Create(AddressView view);
        Task Edit(AddressView view);
        Task Delete(Int32 id);
    }
}
