using Application.Common.Pagination; 
using Application.DTOs.SubCategories;
using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;

namespace Application.Service;

public class SubCategoryService : Service<SubCategory>, ISubCategoryService
{
    private readonly ISubCategoryRepository _repository;

    public SubCategoryService(ISubCategoryRepository repository) : base(repository)
    {
        _repository = repository;
    }
    public async Task<PagedList<SubCategory>> GetSubCategoriesAsync(SubCategoryPageParam pageParam)
    {

        var query = _repository.TableNoTracking.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pageParam.SearchKey))
        {
            string searchKey = pageParam.SearchKey.ToLower().Trim();

            query = query
                .Where(c => c.Name.ToLower().Contains(searchKey)
                || c.Code.Contains(searchKey));
        }

        query = query.OrderBy(c => c.Name);

        return await PagedList<SubCategory>.CreateAsync(query, pageParam.PageSize, pageParam.PageNumber);
    }
}
