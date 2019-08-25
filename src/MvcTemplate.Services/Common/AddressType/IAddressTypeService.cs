using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Services
{
    public interface IAddressTypeService : IService
    {
        Task<TView> Get<TView>(Int32 id) where TView : BaseCodeView;
        IQueryable<AddressTypeView> GetViews();

        Task Create(AddressTypeView view);
        Task Edit(AddressTypeView view);
        Task Delete(Int32 id);
    }
}
