using Application.Common.Pagination;
using Application.DTOs.Categories;
using Application.ServiceInterface.Base;
using Domain.Entities;

namespace Application.ServiceInterface;

public interface ICategoryService : IService<Category>
{
    Task<PagedList<Category>> GetCategoriesAsync(CategoryPageParam pageParam);
}
