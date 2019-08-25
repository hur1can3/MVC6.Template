using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Services
{
    public interface ICustomerService : IService
    {
        Task<TView> Get<TView>(Int32 id) where TView : BaseActiveSoftDeleteView;
        IQueryable<CustomerView> GetViews();

        Task Create(CustomerView view);
        Task Edit(CustomerView view);
        Task Delete(Int32 id);
    }
}
