using Application.Common.Pagination;
using Application.Common.Result;
using Application.DTOs;
using Application.DTOs.Categories;
using Application.Errors;
using Application.Extentions;
using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    internal class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository
            , ISubCategoryRepository subCategoryRepository
            , IProductRepository productRepository
            , IMapper mapper) : base(repository)
        {
            _repository = repository;
            _subCategoryRepository = subCategoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
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

            query = query.OrderByDescending(c => c.Id);



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

        public async Task<Result> GetCategoryHierarchy(int? size)
        {
            var query = _repository
                      .TableNoTracking
                      .OrderByDescending(c => c.CreatedOn)
                      .Include(c => c.SubCategories)
                      .ThenInclude(s => s.Products)
                      .Select(c => new Hierarchy<long>
                      {
                          Key = c.Id,
                          Value = c.Name,
                          Code = c.Code,
                          Child = c.SubCategories.Select(s => new Hierarchy<long>
                          {
                              Key = s.Id,
                              Value = s.Name,
                              Code = s.Code,
                              Child = s.Products.Select(p => new Hierarchy<long>
                              {
                                  Key = p.Id,
                                  Value = p.Name,
                                  Code = p.Code,
                              })
                          })
                      });

            if (size is not null && size > 0)
            {
                query = query.Take((int)size);
            }

            var tree =await query.ToListAsync();

            if (tree.IsNullOrEmpty())
            {
                return Result.Failure(CategoryError.NotFound);
            }

            return Result.Success(tree);
        }
    }
}
