namespace API.Controllers.Dtos;
public class P_PaymentDto
{
    public string Id { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Total { get; set; }

}
