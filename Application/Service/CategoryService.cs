using Application.Common.Pagination;
using Application.Common.Result;
using Application.DTOs.Categories;
using Application.Errors;
using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    internal class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository repository
            , ISubCategoryRepository subCategoryRepository
            , IProductRepository productRepository) : base(repository)
        {
            _repository = repository;
            _subCategoryRepository = subCategoryRepository;
            _productRepository = productRepository;
        }

        public async Task<PagedList<Category>> GetCategoriesAsync(CategoryPageParam pageParam)
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



            return await PagedList<Category>.CreateAsync(query, pageParam.PageSize, pageParam.PageNumber);
        }
        public async Task<Result> DeleteWithHierarchyAsync(long id, long userId)
        {

            var category = await GetByIdAsync(id);

            if (category is null)
            {
                return Result.Failure(CategoryError.NotFound);
            }


            if (await RemoveAsync(category))
            {
                await _subCategoryRepository.DeleteSubCategoryHierarchy(id, userId);
                return Result.Success();
            }

            return Result.Failure(CategoryError.DeleteFailed);
        }
    }
}
