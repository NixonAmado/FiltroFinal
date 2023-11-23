using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public string ProductId { get; set; }

    public string Cantidad { get; set; }

    public decimal UnitPrice { get; set; }

    public short LineNumber { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }

}
