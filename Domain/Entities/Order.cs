using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly ExpectedDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public string Status { get; set; }

    public string Comments { get; set; }

    public int? CustomerId { get; set; }

    public string PaymentId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual Employee Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Payment Payment { get; set; }
}
