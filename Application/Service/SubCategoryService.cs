using Application.Common.Pagination;
using Application.Common.Result;
using Application.DTOs.SubCategories;
using Application.Errors;
using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;

namespace Application.Service;

internal class SubCategoryService : Service<SubCategory>, ISubCategoryService
{
    private readonly ISubCategoryRepository _repository;
    private readonly IProductRepository _productRepository;

    public SubCategoryService(ISubCategoryRepository repository
        , IProductRepository productRepository) : base(repository)
    {
        _repository = repository;
        _productRepository = productRepository;
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

        query = query.OrderByDescending(c => c.Id);

        return await PagedList<SubCategory>.CreateAsync(query, pageParam.PageSize, pageParam.PageNumber);
    }
    public async Task<Result> DeleteSubCategoryWithHierarchy(long subcategoryId, long userId)
    {
        if (subcategoryId <= 0)
        {
            return Result.Failure(CommonError.InvalidRequest);
        }

        var subcategory = await _repository.GetByIdAsync(subcategoryId);
        if (subcategory is null)
        {
            return Result.Failure(SubCategoryError.NotFound);
        }
        if (await _repository.RemoveAsync(subcategory))
        {
            await _productRepository.BulkSoftDeleteByProductsAsync(new List<long> { subcategoryId }, userId);
            return Result.Success();
        }
        return Result.Failure(SubCategoryError.DeleteFailed);
    }
}
