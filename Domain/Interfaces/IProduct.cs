using Domain.Entities;

namespace Domain.Interfaces;
public interface IProduct : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetByNotOrdered();
    Task<IEnumerable<object>> GetTotalSalesByRangeIva(decimal range);
    Task<object> GetByMostSold();
    Task<IEnumerable<object>> GetByMostSoldLimit();
    Task<IEnumerable<object>> GetGama();
}
