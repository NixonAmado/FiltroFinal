using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class CustomerRepository : GenericRepository<Customer>, ICustomer
    {
        private readonly DbAppContext _context;

        public CustomerRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
        //1.Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no han realizado ningun pedido.
        public async Task<IEnumerable<object>> GetWithOrdersQuantity()
        {
            return await _context.Customers.Select(p => new
            {
                p.Name,
                OrdersQuantity = p.Orders.Count()
            }).ToListAsync();
        }

    //Devuelve el nombre de los cliente  a los que no  se les  ha entregado a tiempo un pedido
        public async Task<IEnumerable<object>> GetNameNotDeliveratedOnTime()
        {
            
            return await _context.Customers
            .Where(p => p.Orders.Any(o  => o.DeliveryDate > o.ExpectedDate))
            .Select(p => new
            {
                p.Name
            }).ToListAsync();
        }
    public override async Task<(int totalRegistros, IEnumerable<Customer> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Customers as IQueryable<Customer>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Name.ToLower() == search.ToLower());

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