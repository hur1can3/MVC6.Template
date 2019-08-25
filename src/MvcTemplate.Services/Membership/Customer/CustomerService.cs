using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<TView> Get<TView>(Int32 id) where TView : BaseActiveSoftDeleteView
        {
            return await UnitOfWork.GetAsAsync<Customer, TView>(id);
        }

        public IQueryable<CustomerView> GetViews()
        {
            return UnitOfWork
                .Select<Customer>()
                .To<CustomerView>()
                .OrderByDescending(customer => customer.Id);
        }

        public async Task Create(CustomerView view)
        {
            Customer customer = UnitOfWork.To<Customer>(view);

            await UnitOfWork.InsertAsync(customer);
            await UnitOfWork.SaveChangesAsync();
        }
        public async Task Edit(CustomerView view)
        {
            Customer customer = UnitOfWork.To<Customer>(view);

            UnitOfWork.Update(customer);
            await UnitOfWork.SaveChangesAsync();
        }
        public async Task Delete(Int32 id)
        {
            UnitOfWork.Delete<Customer>(id);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
