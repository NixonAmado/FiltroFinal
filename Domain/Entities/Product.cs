using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Product
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Gama { get; set; }

    public string Supplier { get; set; }

    public string Dimentions { get; set; }

    public string Description { get; set; }

    public int Stock { get; set; }

    public decimal SalePrice { get; set; }

    public decimal SupplierPrice { get; set; }

    public virtual ProductGama GamaNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
