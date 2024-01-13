using Application.DTOs.Tests;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TestCreateDto, Test>();
        }
    }
}
