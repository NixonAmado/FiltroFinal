using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class EmployeeRepository : GenericRepository<Employee>, IEmployee
    {
        private readonly DbAppContext _context;

        public EmployeeRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
    //6.Devuelve el nombre. apellidos, puesto y telelfono de oficiona de aquellos empleados que no sean representantes de ventas de ningún cliente.
    public async Task<IEnumerable<object>> GetByNotCustomer()
    {
        return await _context.Employees
                            .Where(e => e.JobTitle == "REPRESENTANTE VENTAS")
                            .Select(e => new
                            {
                                e.Name,
                                LastName = e.LastName1 + " " + e.LastName2,
                                e.JobTitle,
                                e.Office.Phone
                            }).ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Employee> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Employees as IQueryable<Employee>;
    
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