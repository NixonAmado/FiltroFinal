using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class PaymentMethodRepository : GenericRepository<PaymentMethod>, IPaymentMethod
    {
        private readonly DbAppContext _context;

        public PaymentMethodRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
    public override async Task<(int totalRegistros, IEnumerable<PaymentMethod> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.PaymentMethods as IQueryable<PaymentMethod>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Description.ToLower() == search.ToLower());

                }
    
                query = query.OrderBy(p => p.Description);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }        

    }