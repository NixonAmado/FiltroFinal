namespace API.Controllers.Dtos;
public class P_OrderDto
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly ExpectedDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public string Status { get; set; }

    public string Comments { get; set; }
}
public class OrderNotDeliveratedDto
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }
    public DateOnly ExpectedDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

}
