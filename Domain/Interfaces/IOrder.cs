using Domain.Entities;

namespace Domain.Interfaces;
public interface IOrder : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetWithDataByOrderNotDelivered();   
    
}
