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
using Microsoft.AspNetCore.Authorization;

namespace eCom.API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IPhotoService _photoService;

        public CategoriesController(ICategoryService categoryService
            , IMapper mapper
            , ICurrentUserService currentUser
            , IPhotoService photoService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _currentUser = currentUser;
            _photoService = photoService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateDto dto)
        {
            if (dto is null)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var photoUploadResult = await _photoService.AddPhotoAsync(dto.Image);
            if (photoUploadResult.Error != null)
            {
                return BadRequest(Result.Failure(CategoryError.ImageUploadFailed));
            }

            var category = _mapper.Map<Category>(dto);
            if (photoUploadResult.SecureUrl != null)
            {
                category.ImageUrl = photoUploadResult.SecureUrl.AbsoluteUri;
                category.PhotoPublicId = photoUploadResult.PublicId;
            }

            if (await _categoryService.AddAsync(category))
            {
                var routeValues = new
                {
                    action = nameof(GetCategoryById),
                    controller = "Categories",
                    id = category.Id,

                };
                return CreatedAtRoute(routeValues, Result.Success(category));
            }
            return BadRequest(Result.Failure(CategoryError.CreateFailed));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryPageParam pageParam)
        {
            var categories = await _categoryService.GetCategoriesAsync(pageParam);

            if (categories is null || !categories.Any())
            {
                return NotFound(Result.Failure(CategoryError.NotFound));
            }
            Response.AddPaginationHeader(categories.CurrentPage, categories.PageSize, categories.TotalCount, categories.TotalPage);

            var result = _mapper.Map<List<CategoryDto>>(categories).SetSlNumber(categories.CurrentPage, categories.PageSize);

            return Ok(result);
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

            return Ok(Result.Success(_mapper.Map<CategoryDto>(category)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryById(long id, [FromForm] CategoryDto dto)
        {
            if (id <= 0 || dto is null)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var category = await _categoryService.GetByIdAsync(id);

            if (category is null)
            {
                return NotFound(Result.Failure(CategoryError.NotFound));
            }

            var photoUploadResult = await _photoService.AddPhotoAsync(dto.Image);
            if (photoUploadResult.Error != null)
            {
                return BadRequest(Result.Failure(CategoryError.ImageUploadFailed));
            }

            _mapper.Map(dto, category, opt => opt.AfterMap((src, des) =>
            {
                des.Id = id;
                if (photoUploadResult.SecureUrl != null)
                {
                    category.ImageUrl = photoUploadResult.SecureUrl.AbsoluteUri;
                    category.PhotoPublicId = photoUploadResult.PublicId;
                }
            }));

            if (await _categoryService.UpdateAsync(category))
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

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories.IsNullOrEmpty())
            {
                return NotFound(Result.Failure(CategoryError.NotFound));
            }

            return Ok(Result.Success(categories));
        }
        [HttpGet("hierarchy")]
        public async Task<ActionResult<Result>> GetCategoryHierarchy([FromQuery] int? size)
        {
            return await _categoryService.GetCategoryHierarchy(size);
        }
    }
}
