using Application.DTOs.Categories;
using Application.DTOs.Products;
using Application.DTOs.SubCategories;
using Application.DTOs.Tests;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.User;

namespace Application.Common.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TestCreateDto, Test>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<AuthUser, RegisterDto>();
            CreateMap<RegisterDto, AuthUser>();
            CreateMap<AuthUser, AuthUserDto>();

            CreateMap<SubCategoryCreateDto, SubCategory>();
            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();

            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<SectionCreateDto, Section>();
            CreateMap<PricingItemCreateDto,PricingItem>();

            CreateMap<Section,SectionDto>().ReverseMap();
            CreateMap<PricingItem,PricingItemDto>().ReverseMap();
        }
    }
}
