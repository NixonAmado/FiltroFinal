using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class ProductGamaRepository : GenericRepository<ProductGama>, IProductGama
    {
        private readonly DbAppContext _context;

        public ProductGamaRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
    public override async Task<(int totalRegistros, IEnumerable<ProductGama> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.ProductGamas as IQueryable<ProductGama>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Gama.ToLower() == search.ToLower());

                }
    
                query = query.OrderBy(p => p.Gama);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }        

    }