namespace API.Controllers.Dtos;
public class P_CustomerDto
{
   public int Id { get; set; }

    public string Name { get; set; }

    public string ContactName { get; set; }

    public string ContactLastName { get; set; }

    public string Phone { get; set; }

    public string Fax { get; set; }

    public int? AddressId { get; set; }

    public decimal? CreditLimit { get; set; }

}
