# FiltroFinal



#### Pasos para el volcado de datos a la bd
1. Para este proyecto se requiere el uso de mySql Workbench
2. Entra a Api y ejecuta el comando dotnet run, si no funciona puedes hacer uso del script Update para que ef convierta tu estructura orienta a objetos a una tabular en la bd.
3. Una tengas las tablas que vas a usar y que todo salió bien, puedes hacer el volcado de datos dirigiendote a [Datos](./Datos.md)
- Una vez adentro de Datos puedes hacer ctrl + A para copiar todo el texto.
- Vas a Workbench y elijes la bd llamada jardineria2, abres un archivo SQL y le das a ctrl + V
- Señala todos los datos con ctrl + A y le das al rayo que apartece en el menú de ejecución que se encuentra en la parte superior del archivo
- Y ya quedaría el volcado de datos echo.

1.Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no han realizado ningun pedido.
```
/api/customer/GetWithOrdersQuantity
```
public async Task<IEnumerable<object>> GetWithOrdersQuantity()
        {
            return await _context.Customers.Select(p => new
            {
                p.Name,
                OrdersQuantity = p.Orders.Count()
            }).ToListAsync();
        }

Explicacion: Se crea un objeto que tendrá dos atributos, uno es el nombre y el otro es la cantidad de ordernes asociadas a este, por medio de la coleccion de navegacion que el cliente tiene asociada para posteriormente contarla con el metodo Count()

Se crea como objeto, ya que, para este enpoint necesito una entidad que no esta en la entidad original.


2.Devuelve un listado con el código de pedido, codigo cliente, fecha esperada y fehca de entrega de los pedidos que no han sido eentregados a tiempo
```
/api/order/GetWithDataByOrderNotDelivered
```
public async Task<IEnumerable<Order>> GetWithDataByOrderNotDelivered()
{
    return await _context.Orders
    .Where(o => o.DeliveryDate > o.ExpectedDate && o.DeliveryDate != null) 
    .Include(p => p.Customer)
    .ToListAsync();
}

Se aplica un Filtro que indica devuelve true si la fecha de entrega es mayor a la fecha esperada y además no es nula.
Se crea un dto para esta enpoint ya que tengo todos los atributos que necesito.
Se hace el include de customer para poder traerme el id asociado a este.


3.Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto
```
/api/product/GetByNotOrdered
```
public async Task<IEnumerable<Product>> GetByNotOrdered()
{
    return await _context.Products
                        .Include(p => p.GamaNavigation)
                        .Where(p => !p.OrderDetails.Any())
                        .ToListAsync();
}
Se incluye la propiedad de navegacion gama, ya que, se asimila que la img es la que esta asociada a la gama, según la referencia en la bd.
Se crea un dto para esta enpoint ya que tengo todos los atributos que necesito.
Se aplica un filtro con where, donde se aplica un any para decir que si el producto  tiene algun registro de  orderdetails lo devuelva, pero con el caracter de exclamacion revertimos la respuesta si ningun producto tiene asociada alguna orden

4.Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizada la compra de algún producto de la gama X
```
api/office/GetByNotAssociatedEmployeeToGamaP/{gama}
```
public async Task<IEnumerable<Office>> GetByNotAssociatedEmployeeToGamaP(string gama)
{
    return await _context.Offices 
                        .Include(o => o.Address)
                        .Where(o => o.Employees.Any(e => e.JobTitle.ToUpper() == "REPRESENTANTE VENTAS" &&
                         !e.Orders.Any(o => o.OrderDetails.Any(od => od.Product.Gama.ToUpper() == gama))))
                        .ToListAsync();
}
Se crea un dto para esta enpoint ya que tengo todos los atributos que necesito.
Se incluye la referencia a la direccion para que pueda ser mappeado por medio del include
Se selecciona todos los empleados que tengan como cargo "Representante ventas" y se usa el any para revisar que la propiedad de navegacion contenga por lo menos un registro, y cuando llegamos al la gama del producto, se compara con el parametro gama
Al momento de usar el signo de admiracion le cambiamos el retorno a todos aquellas propiedades de navegacion donde no haya un registro de este.


5.Lista las ventas totales de los productos que hayan facturado más de 3000 euros. Se mostrará el nombre, unit vendidas, total facturado y total con impuestos (21%Iva)

```
/api/product/GetTotalSalesByRangeIva/{rango}
```
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
No pudo crear un dto ya que tiene propiedades adicionales 
Se agrupa por el id de los productos para así obtener una agrupacios de todos los productos relacionados
Se selecciona el nombre de uno de los productos de la agrupacion
Se suman todas las cantidades vendidas por cada producto. ya que la cantidad es un string se tiene que convertir a entero por medioi de Convert.ToInt32
En el total se multiplica el precio por unidad por la cantidad para así obtener el total, y con math round se redondean los decimales a dos
el total con impuestos se hace lo mismo con la diferencia que despues de la suma se multiplica 1.21 para sacar el precio total con Iva, tambien se castea la suma para que pueda ser compatible con el 1.21 que es decimal y ademas el rango de precio es variable
se ordena por el precio total con impuestos



6.Devuelve el nombre. apellidos, puesto y telelfono de oficiona de aquellos empleados que no sean representantes de ventas de nungun cliente.
```
/api/employee/GetByNotCustomer
```
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

Se crea un objeto por que me estoy quedando corto de tiempo, los dos enpoints anteriores eran los más largos de todos los 63 y ademas los hice variables, Profe tengame piedad. 
Se filtra por empleados con el cargo representante ventas
Se crea un objeto anonimo que almacena los atributos solicitados 

7.Devuelve el nombre del producto del que se han vendido mas unidades.(Tenga en cuenta que tendra que calcular cual es el numero total de unidades que se han vendido de cada producto a partir de los datos de la tabla detalle pedido)
```
/api/product/GetByMostSold
```
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

Se agrupa por productId
Se seleccciona la agrupacion y se elije el nombre del primer grupo asociado
Se calcula La unidad vendida
Se ordena de mayor a menor por las unidades vendidas y se selecciona el primero 


8.Devuelve un listado de los 20 productos mas vendidos y el numero total de unidades que se han vendido de cada uno. El listado debera estar ordenado por el numero total de unidades vendidas.

```
/api/product/GetByMostSoldLimit
```   
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
    

Se agrupa por productId
Se seleccciona la agrupacion y se elije el nombre del primer grupo asociado
Se calcula La unidad vendida
Se ordena de mayor a menos y con take tomamos 20 y despues se agrupan con list.


9.Devuelve el nombre de los cliente  a los que no  se les  ha entregado a tiempo un pedido
```
/api/customer/GetNameNotDeliveratedOnTime
```   
public async Task<IEnumerable<object>> GetNameNotDeliveratedOnTime()
{
    
    return await _context.Customers
    .Where(p => p.Orders.Any(o  => o.DeliveryDate > o.ExpectedDate))
    .Select(p => new
    {
        p.Name
    }).ToListAsync();
}

se aplica un filtro donde la fecha de entraga sea mayor que la esperada
y se proyecta el nombre con un objeto anonimo

Devuelve  un listado de las diferentes gamas  de producto   que ha comprado un cliente
```
/api/product/GetGama
```   
    public async Task<IEnumerable<object>> GetGama()
    {
        return await _context.Products
                            .Select(p => new {
                                p.GamaNavigation.Gama
                            })
                            .Distinct()
                            .ToListAsync();
    }

Se proyecta un objeto anonimo donde se selecciona la gama vinculada al producto y con distint se eliminar las posibles concurrencias
    

