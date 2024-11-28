using AutoMapper;
using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;

namespace GenericStore.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Store, StoreDTO>();
        CreateMap<StoreDTO, Store>();
        CreateMap<StoreProduct, StoreProductDTO>();
        CreateMap<StoreProductDTO, StoreProduct>();
        CreateMap<StoreDTO, Store>();
        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();
        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();
        CreateMap<OrderDetail, OrderDetailDTO>();
        CreateMap<OrderDetailDTO, OrderDetail>();
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
}