﻿using Application.Common.Result;
using Application.DTOs.Products;
using Application.DTOs.SubCategories;
using Application.Errors;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities;
using eCom.API.Controllers.Base;
using Application.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace eCom.API.Controllers
{
     
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService
            , IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto dto)
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
            product.CreatedById = 1;
            product.CreatedOn = DateTime.UtcNow;
            product.Code = new Random().Next(1, 100).ToString();


            if (await _productService.AddAsync(product))
            {
                var routeValues = new
                {
                    action = nameof(GetProductById),
                    controller = "Products",
                    id = product.Id,

                };
                return CreatedAtRoute(routeValues, _mapper.Map<ProductDto>(product));
            }
            return BadRequest(Result.Failure(ProductError.CreateFailed));
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductPageParam pageParam)
        {
            var products = await _productService.GetProductsAsync(pageParam);

            if (products is null || !products.Any())
            {
                return NotFound(Result.Failure(ProductError.NotFound));
            }
            Response.AddPaginationHeader(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPage);
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }

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

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductById(long id, [FromBody] ProductDto dto)
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

            _mapper.Map(dto, product, opt => opt.AfterMap((src, des) =>
            {
                des.Id = id;
            }));

            if (await _productService.UpdateAsync(product))
            {
                var routeValues = new
                {
                    action = nameof(GetProductById),
                    controller = "Products",
                    id = product.Id,

                };
                return CreatedAtRoute(routeValues, product);
            }
            return BadRequest(Result.Failure(ProductError.UpdateFailed(product.Code)));
        }
    }
}
