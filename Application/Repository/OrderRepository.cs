using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class OrderRepository : GenericRepository<Order>, IOrder
    {
        private readonly DbAppContext _context;

        public OrderRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
        //2.Devuelve un listado con el código de pedido, codigo cliente, fecha esperada y fehca de entrega de los pedidos que no han sido eentregados a tiempo
        public async Task<IEnumerable<Order>> GetWithDataByOrderNotDelivered()
        {
            return await _context.Orders
            .Where(o => o.DeliveryDate > o.ExpectedDate && o.DeliveryDate != null)
            .Include(p => p.Customer)
            .ToListAsync();
        }
    public override async Task<(int totalRegistros, IEnumerable<Order> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Orders as IQueryable<Order>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Id.ToString() == search.ToLower());

                }
    
                query = query.OrderBy(p => p.Id);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }        

    }