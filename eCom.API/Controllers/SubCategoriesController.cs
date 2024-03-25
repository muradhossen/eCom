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
using Application.Common.CurrentUser;
using Application.Service;

namespace eCom.API.Controllers
{

    public class SubCategoriesController : BaseApiController
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IPhotoService _photoService;

        public SubCategoriesController(ISubCategoryService subCategoryService
            , IMapper mapper
            , ICurrentUserService currentUser
            , IPhotoService photoService)
        {
            _subCategoryService = subCategoryService;
            _mapper = mapper;
            _currentUser = currentUser;
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromForm] SubCategoryCreateDto dto)
        {
            if (dto is null)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }
            var subCategory = _mapper.Map<SubCategory>(dto); 

            var photoUploadResult = await _photoService.AddPhotoAsync(dto.Image);
            if (photoUploadResult.Error != null)
            {
                return BadRequest(Result.Failure(SubCategoryError.ImageUploadFailed));
            }
            if (photoUploadResult.SecureUrl != null)
            {
                subCategory.ImageUrl = photoUploadResult.SecureUrl.AbsoluteUri;
                subCategory.PhotoPublicId = photoUploadResult.PublicId;
            }
            if (await _subCategoryService.AddAsync(subCategory))
            {
                var routeValues = new
                {
                    action = nameof(GetSubCategoryById),
                    controller = "SubCategories",
                    id = subCategory.Id,

                };
                return CreatedAtRoute(routeValues, Result.Success(subCategory));
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

            var result = _mapper.Map<List<SubCategoryDto>>(subCategories)
                  .SetSlNumber(subCategories.CurrentPage, subCategories.PageSize);

            return Ok(result);
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

            return Ok(Result.Success(_mapper.Map<SubCategoryDto>(subCategory)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategoryById(long id, [FromForm] SubCategoryDto dto)
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

            var photoUploadResult = await _photoService.AddPhotoAsync(dto.Image);
            if (photoUploadResult.Error != null)
            {
                return BadRequest(Result.Failure(SubCategoryError.ImageUploadFailed));
            } 

            _mapper.Map(dto, subCategory, opt => opt.AfterMap((src, des) =>
            {
                des.Id = id;
                if (photoUploadResult.SecureUrl != null)
                {
                    subCategory.ImageUrl = photoUploadResult.SecureUrl.AbsoluteUri;
                    subCategory.PhotoPublicId = photoUploadResult.PublicId;
                }
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

            var result = await _subCategoryService.DeleteSubCategoryWithHierarchy(id, _currentUser.UserId);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }
    }
}
