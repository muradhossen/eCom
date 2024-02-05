using Application.Common.Result;
using Application.DTOs.Categories;
using Application.Errors;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities;
using eCom.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Application.Extentions;
using Application.Common.CurrentUser;

namespace eCom.API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CategoriesController(ICategoryService categoryService
            , IMapper mapper
            , ICurrentUserService currentUser)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto dto)
        {
            if (dto is null)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }
            var category = _mapper.Map<Category>(dto);
            category.CreatedById = 1;
            category.CreatedOn = DateTime.UtcNow;
            category.Code = new Random().Next(1,100).ToString();


            if (await _categoryService.AddAsync(category))
            {
                var routeValues = new
                {
                    action = nameof(GetCategoryById),
                    controller = "Categories",
                    id = category.Id,
                    
                };
                return CreatedAtRoute(routeValues, category);
            }
            return BadRequest(Result.Failure(CategoryError.CreateFailed));
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryPageParam pageParam)
        {
            var categories = await _categoryService.GetCategoriesAsync(pageParam);

            if (categories is null || !categories.Any())
            {
                return NotFound(Result.Failure(CategoryError.NotFound));
            }
            Response.AddPaginationHeader(categories.CurrentPage, categories.PageSize, categories.TotalCount, categories.TotalPage);
            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

           var category = await _categoryService.GetByIdAsync(id);

            if (category is null)
            {
                return NotFound(Result.Failure(CategoryError.NotFound));
            }

            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryById(long id, [FromBody] CategoryDto dto)
        {
            if (id <= 0 || dto is null)
            {
                return BadRequest("Invalid request!");
            }

            var category = await _categoryService.GetByIdAsync(id);

            if (category is null)
            {                 
              return NotFound(Result.Failure(CategoryError.NotFound));
            }

            _mapper.Map(dto,category,opt=> opt.AfterMap((src, des) =>
            {
                des.Id = id; 
            }));

            if(await _categoryService.UpdateAsync(category))
            {
                var routeValues = new
                {
                    action = nameof(GetCategoryById),
                    controller = "Categories",
                    id = category.Id,

                };
                return CreatedAtRoute(routeValues, category);
            }
            return BadRequest(Result.Failure(CategoryError.UpdateFailed(category.Code)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var result = await _categoryService.DeleteWithHierarchyAsync(id, _currentUser.UserId);

            if (result.IsFailure)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
