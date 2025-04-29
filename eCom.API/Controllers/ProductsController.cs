using Application.Common.Result;
using Application.DTOs.Products;
using Application.DTOs.SubCategories;
using Application.Errors;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities;
using eCom.API.Controllers.Base;
using Application.Extentions;
using Microsoft.AspNetCore.Mvc;
using Application.Service;
using Microsoft.AspNetCore.Authorization;

namespace eCom.API.Controllers
{
     
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public ProductsController(IProductService productService
            , IMapper mapper
            , IPhotoService photoService)
        {
            _productService = productService;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto dto)
        {
            if (dto is null)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            if (dto.Section is null || dto.Section.PricingItems.IsNullOrEmpty())
            {
                return BadRequest(Result.Failure(ProductError.NoPricintItemToCreate));
            }

            var product = _mapper.Map<Product>(dto);

            var photoUploadResult = await _photoService.AddPhotoAsync(dto.Image);
            if (photoUploadResult.Error != null)
            {
                return BadRequest(Result.Failure(SubCategoryError.ImageUploadFailed));
            }
            if (photoUploadResult.SecureUrl != null)
            {
                product.ImageUrl = photoUploadResult.SecureUrl.AbsoluteUri;
                product.PhotoPublicId = photoUploadResult.PublicId;
            }

            if (await _productService.AddAsync(product))
            {
                var routeValues = new
                {
                    action = nameof(GetProductById),
                    controller = "Products",
                    id = product.Id,

                };
                return CreatedAtRoute(routeValues, Result.Success(_mapper.Map<ProductDto>(product)));
            }
            return BadRequest(Result.Failure(ProductError.CreateFailed));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductPageParam pageParam)
        {
            var products = await _productService.GetProductsAsync(pageParam);

            if (products is null || !products.Any())
            {
                return NotFound(Result.Failure(ProductError.NotFound));
            }
            Response.AddPaginationHeader(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPage);
            return Ok(Result.Success(_mapper.Map<List<ProductDto>>(products)));
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var product = await _productService.GetByIdAsync(id);

            if (product is null)
            {
                return NotFound(Result.Failure(ProductError.NotFound));
            }

            return Ok(Result.Success(_mapper.Map<ProductDto>(product)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductById(long id, [FromForm] ProductDto dto)
        {
            if (id <= 0 || dto is null)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var product = await _productService.GetByIdAsync(id);

            if (product is null)
            {
                return NotFound(Result.Failure(ProductError.NotFound));
            }

            var photoUploadResult = await _photoService.AddPhotoAsync(dto.Image);
            if (photoUploadResult.Error != null)
            {
                return BadRequest(Result.Failure(ProductError.ImageUploadFailed));
            }

            _mapper.Map(dto, product, opt => opt.AfterMap((src, des) =>
            {
                des.Id = id;
                if (photoUploadResult.SecureUrl != null)
                {
                    product.ImageUrl = photoUploadResult.SecureUrl.AbsoluteUri;
                    product.PhotoPublicId = photoUploadResult.PublicId;
                }
                des.Section.ProductId = id; 
            }));

            if (await _productService.UpdateAsync(product))
            {
                var routeValues = new
                {
                    action = nameof(GetProductById),
                    controller = "Products",
                    id = product.Id,

                };
                return CreatedAtRoute(routeValues, dto);
            }
            return BadRequest(Result.Failure(ProductError.UpdateFailed(product.Code)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var product = await _productService.GetByIdAsync(id);

            if (product is null)
            {
                return NotFound(Result.Failure(ProductError.NotFound));
            }

            if (await _productService.RemoveAsync(product))
            {
                return Ok(Result.Success());
            }
            return BadRequest(Result.Failure(CommonError.UnknownError));
        }
    }
}
