namespace API.Controllers.Dtos;
public class P_OfficeDto
{
    public string Id { get; set; }
    public string Phone { get; set; }

}

public class EssencialOfficeDto
{
    public string Id { get; set; }
    public string Phone { get; set; }
    public P_AddressDto Address { get; set; }
}