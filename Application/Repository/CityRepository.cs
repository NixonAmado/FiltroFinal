using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class CityRepository : GenericRepository<City>, ICity
    {
        private readonly DbAppContext _context;

        public CityRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
    public override async Task<(int totalRegistros, IEnumerable<City> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Cities as IQueryable<City>;
    
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