using Application.Common.Pagination;
using Application.DTOs.Categories;
using Application.RepositoryInterface;
using Application.RepositoryInterface.Base;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;

namespace Application.Service
{
    internal class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository) : base(repository)
        {
            _repository = repository;
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



            return await PagedList<Category>.CreateAsync(query,pageParam.PageSize,pageParam.PageNumber);
        }
    }
}
