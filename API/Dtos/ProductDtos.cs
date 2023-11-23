namespace API.Controllers.Dtos;
public class P_ProductDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Gama { get; set; }

    public string Supplier { get; set; }

    public string Dimentions { get; set; }

    public string Description { get; set; }

    public int Stock { get; set; }

    public decimal SalePrice { get; set; }

    public decimal SupplierPrice { get; set; }

}
public class ProductWithImageDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public  ProductGamaImgDto GamaNavigation { get; set; }


}
