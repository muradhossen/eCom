using Application.Common.Result;
using Application.DTOs.Categories;
using Application.DTOs.SubCategories;
using Application.Errors;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities;
using eCom.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Application.Extentions;

namespace eCom.API.Controllers
{
 
    public class SubCategoriesController : BaseApiController
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly IMapper _mapper;

        public SubCategoriesController(ISubCategoryService subCategoryService
            , IMapper mapper)
        {
            _subCategoryService = subCategoryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromBody] SubCategoryCreateDto dto)
        {
            if (dto is null)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }
            var subCategory = _mapper.Map<SubCategory>(dto);
            subCategory.CreatedById = 1;
            subCategory.CreatedOn = DateTime.UtcNow;
            subCategory.Code = new Random().Next(1, 100).ToString();


            if (await _subCategoryService.AddAsync(subCategory))
            {
                var routeValues = new
                {
                    action = nameof(GetSubCategoryById),
                    controller = "SubCategories",
                    id = subCategory.Id,

                };
                return CreatedAtRoute(routeValues, subCategory);
            }
            return BadRequest(Result.Failure(SubCategoryError.CreateFailed));
        }
        [HttpGet]
        public async Task<IActionResult> GetSubCategories([FromQuery] SubCategoryPageParam pageParam)
        {
            var subCategories = await _subCategoryService.GetSubCategoriesAsync(pageParam);

            if (subCategories is null || !subCategories.Any())
            {
                return NotFound(Result.Failure(SubCategoryError.NotFound));
            }
            Response.AddPaginationHeader(subCategories.CurrentPage, subCategories.PageSize, subCategories.TotalCount, subCategories.TotalPage);
            return Ok(_mapper.Map<List<SubCategoryDto>>(subCategories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryById(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var subCategory = await _subCategoryService.GetByIdAsync(id);

            if (subCategory is null)
            {
                return NotFound(Result.Failure(SubCategoryError.NotFound));
            }

            return Ok(_mapper.Map<SubCategoryDto>(subCategory));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategoryById(long id, [FromBody] SubCategoryDto dto)
        {
            if (id <= 0 || dto is null)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var subCategory = await _subCategoryService.GetByIdAsync(id);

            if (subCategory is null)
            {
                return NotFound(Result.Failure(SubCategoryError.NotFound));
            }

            _mapper.Map(dto, subCategory, opt => opt.AfterMap((src, des) =>
            {
                des.Id = id;
            }));

            if (await _subCategoryService.UpdateAsync(subCategory))
            {
                var routeValues = new
                {
                    action = nameof(GetSubCategoryById),
                    controller = "SubCategories",
                    id = subCategory.Id,

                };
                return CreatedAtRoute(routeValues, subCategory);
            }
            return BadRequest(Result.Failure(SubCategoryError.UpdateFailed(subCategory.Code)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var subcategory = await _subCategoryService.GetByIdAsync(id);

            if (subcategory is null)
            {
                return NotFound(Result.Failure(SubCategoryError.NotFound));
            }

            if (await _subCategoryService.RemoveAsync(subcategory))
            {
                return Ok(Result.Success());
            }
            return BadRequest(Result.Failure(CommonError.UnknownError));
        }
    }
}
