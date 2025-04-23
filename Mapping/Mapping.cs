using AutoMapper;
using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;

namespace Fintelex_Assignment.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category Mappings
            CreateMap<Category, CategoryDto>().ReverseMap();

            // Product Mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
            CreateMap<ProductDto, Product>();

            // Customer Mappings
            CreateMap<Customer, CustomerDto>().ReverseMap();

            // Order Mappings
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src =>
                    src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}" : null));
            CreateMap<OrderDto, Order>();

            // OrderItem Mappings
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src =>
                    src.Product != null ? src.Product.Name : null));
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
