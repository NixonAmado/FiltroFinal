using API.Controllers.Dtos;


using AutoMapper;
using Domain.Entities;
namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<Address, P_AddressDto>().ReverseMap();
        CreateMap<City, P_CityDto>().ReverseMap();
        CreateMap<Country, P_CountryDto>().ReverseMap();
        CreateMap<Customer, P_CustomerDto>().ReverseMap();
        CreateMap<Employee, P_EmployeeDto>().ReverseMap();
        CreateMap<Office, P_OfficeDto>().ReverseMap();
        CreateMap<Office, EssencialOfficeDto>().ReverseMap();
        CreateMap<Order, P_OrderDto>().ReverseMap();
        CreateMap<Order, OrderNotDeliveratedDto>().ReverseMap();  
        CreateMap<Payment, P_PaymentDto>().ReverseMap();
        CreateMap<PaymentMethod, P_PaymentMethodDto>().ReverseMap();
        CreateMap<Product, P_ProductDto>().ReverseMap();
        CreateMap<Product, ProductWithImageDto>().ReverseMap();   
        CreateMap<ProductGama, P_ProductGamaDto>().ReverseMap();
        CreateMap<ProductGama, ProductGamaImgDto>().ReverseMap();
        CreateMap<State, P_StateDto>().ReverseMap();

    }
}