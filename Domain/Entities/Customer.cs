using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string ContactName { get; set; }

    public string ContactLastName { get; set; }

    public string Phone { get; set; }

    public string Fax { get; set; }

    public int? AddressId { get; set; }

    public decimal? CreditLimit { get; set; }

    public virtual Address Address { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
