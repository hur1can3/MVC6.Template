using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Services
{
    public interface IContactTypeService : IService
    {
        Task<TView> Get<TView>(Int32 id) where TView : BaseCodeView;
        IQueryable<ContactTypeView> GetViews();

        Task Create(ContactTypeView view);
        Task Edit(ContactTypeView view);
        Task Delete(Int32 id);
    }
}
