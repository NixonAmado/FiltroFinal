using Domain.Entities;

namespace Domain.Interfaces;
public interface ICustomer : IGenericRepository<Customer>
{
     Task<IEnumerable<object>> GetWithOrdersQuantity();
     Task<IEnumerable<object>> GetNameNotDeliveratedOnTime();
}
