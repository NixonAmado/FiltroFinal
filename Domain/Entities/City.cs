using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int StateId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual State State { get; set; }
}
