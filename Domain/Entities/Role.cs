using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Description { get; set; }

    public virtual ICollection<User> IdUserFks { get; set; } = new List<User>();
}
