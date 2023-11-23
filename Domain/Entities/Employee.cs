using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string LastName1 { get; set; }

    public string LastName2 { get; set; }

    public string Extension { get; set; }

    public string Email { get; set; }

    public string OfficeId { get; set; }

    public int? BossId { get; set; }

    public string JobTitle { get; set; }

    public virtual Employee Boss { get; set; }

    public virtual ICollection<Employee> InverseBoss { get; set; } = new List<Employee>();

    public virtual Office Office { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
