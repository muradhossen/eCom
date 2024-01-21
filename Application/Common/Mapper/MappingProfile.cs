using Application.DTOs.Categories;
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
        }
    }
}
