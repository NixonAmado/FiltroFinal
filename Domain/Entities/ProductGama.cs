using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProductGama
{
    public string Gama { get; set; }

    public string TextDescription { get; set; }

    public string HtmlDescription { get; set; }

    public string Image { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
