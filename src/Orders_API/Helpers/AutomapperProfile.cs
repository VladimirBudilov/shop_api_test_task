using AutoMapper;
using DAL.Entites;
using Orders_API.DTOs;
using Orders_API.DTOs.Requests;
using Orders_API.DTOs.Responses;

namespace Orders_API.Helpers;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<ProductRequestDto, Product>();
        CreateMap<Product, ProductResponseDto>();
        
        CreateMap<OrderRequestDto, Order>()
            .ForMember(d => d.OrderProducts,
                opt 
                    => opt.MapFrom(src => src.Products));

        CreateMap<Order, OrderResponseDto>()
            .ForMember(d => d.Products,
                opt 
                    => opt.MapFrom(src => src.OrderProducts
                        .Select(p => 
                            new ProductInOrderResponseDto(
                                new ProductResponseDto(
                                    p.Product.Id, 
                                    p.Product.Title, 
                                    p.Product.Price
                                ),
                                p.Quantity
                                ))
                        .ToList()));
        
        CreateMap<ProductInOrderRequestDto, OrderProduct>() 
            .ForMember(d => d.ProductId,
                opt 
                    => opt.MapFrom(src => src.Id))
            .ForMember(d => d.Quantity,
                opt 
                    => opt.MapFrom(src => src.Quantity));
    }
}