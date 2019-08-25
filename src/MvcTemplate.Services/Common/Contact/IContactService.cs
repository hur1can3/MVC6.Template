using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Services
{
    public interface IContactService : IService
    {
        Task<TView> Get<TView>(Int32 id) where TView : BaseSoftDeleteView;
        IQueryable<ContactView> GetViews();

        Task Create(ContactView view);
        Task Edit(ContactView view);
        Task Delete(Int32 id);
    }
}
