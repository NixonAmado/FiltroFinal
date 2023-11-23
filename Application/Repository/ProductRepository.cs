using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class ProductRepository : GenericRepository<Product>, IProduct
    {
        private readonly DbAppContext _context;

        public ProductRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
   //3.Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto     
    public async Task<IEnumerable<Product>> GetByNotOrdered()
    {
        return await _context.Products
                            .Include(p => p.GamaNavigation)
                            .Where(p => !p.OrderDetails.Any())
                            .ToListAsync();

    }
    //5.Lista las ventas totales de los productos que hayan facturado más de 3000 euros. Se mostrará el nombre, unit vendidas, total facturado y total con impuestos (21%Iva)
    public async Task<IEnumerable<object>> GetTotalSalesByRangeIva(decimal range)
    {
        return await _context.OrderDetails
                            .GroupBy(p => p.ProductId)
                            .Select(g => new {
                                g.FirstOrDefault().Product.Name,
                                UnitsSold = g.Sum( p => Convert.ToInt32(p.Cantidad)),
                                Total = g.Sum( p=>  Math.Round(p.UnitPrice * Convert.ToInt32(p.Cantidad),2)),
                                TotalWithTaxes = g.Sum(p => Math.Round((double)(p.UnitPrice * Convert.ToInt32(p.Cantidad)) * 1.21, 2)),
                            })
                            .Where(p => p.Total > range)
                            .OrderByDescending(p => p.TotalWithTaxes)
                            .ToListAsync();
    }

    //Devuelve el nombre del producto del que se han vendido mas unidades.(Tenga en cuenta que tendra que calcular cual es el numero total de unidades que se han vendido de cada producto a partir de los datos de la tabla detalle pedido)
    public async Task<object> GetByMostSold()
    {
        return await _context.OrderDetails
                            .GroupBy(p => p.ProductId)
                            .Select(g => new {
                                g.FirstOrDefault().Product.Name,
                                UnitsSold = g.Sum( p => Convert.ToInt32(p.Cantidad)),
                            })
                            .OrderByDescending(p => p.UnitsSold)
                            .FirstOrDefaultAsync();
    }
    //Devuelve un listado de los 20 productos mas vendidos y el numero total de unidades que se han vendido de cada uno. El listado debera estar ordenado por el numero total de unidades vendidas.
    public async Task<IEnumerable<object>> GetByMostSoldLimit()
    {
        return await _context.OrderDetails
                            .GroupBy(p => p.ProductId)
                            .Select(g => new {
                                g.FirstOrDefault().Product.Name,
                                UnitsSold = g.Sum( p => Convert.ToInt32(p.Cantidad)),
                            })
                            .OrderByDescending(p => p.UnitsSold)
                            .Take(20)
                            .ToListAsync();
    }
    
    //10  Devuelve  un listado de las diferentes gamas  de producto   que ha comprado un cliente
    public async Task<IEnumerable<object>> GetGama()
    {
        return await _context.Products
                            .Select(p => new {
                                p.GamaNavigation.Gama
                            })
                            .Distinct()
                            .ToListAsync();
    }
    


    
    public override async Task<(int totalRegistros, IEnumerable<Product> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Products as IQueryable<Product>;
    
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