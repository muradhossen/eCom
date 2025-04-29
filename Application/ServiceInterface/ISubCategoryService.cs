using Application.Common.Pagination;
using Application.Common.Result;
using Application.DTOs.SubCategories;
using Application.ServiceInterface.Base;
using Domain.Entities;

namespace Application.ServiceInterface; 

public interface ISubCategoryService: IService<SubCategory>
{
    Task<PagedList<SubCategory>> GetSubCategoriesAsync(SubCategoryPageParam pageParam);
    Task<Result> DeleteSubCategoryWithHierarchy(long subcategoryId, long userId);
}
