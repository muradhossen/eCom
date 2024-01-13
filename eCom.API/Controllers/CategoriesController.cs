using Application.DTOs.Categories;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities;
using eCom.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace eCom.API.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService
            , IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto dto)
        {
            if (dto is null)
            {
                return BadRequest("Invalid data");
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
            return BadRequest("Failed to create category!");
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();

            if (categories is null || !categories.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(long id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid request!");
            }

           var category = await _categoryService.GetByIdAsync(id);

            if (category is null)
            {
                return NotFound("The requested category was not found!");
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
                return BadRequest("The requested category was not found!");
            }

            _mapper.Map(dto,category,opt=> opt.AfterMap((src, des) =>
            {
                des.Id = id;
                des.CreatedOn = DateTime.UtcNow;
                des.CreatedById = 1;
                des.ModifiedOn = DateTime.UtcNow;
                des.ModifiedById = 2;
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
            return BadRequest("Failed to update category!");
        }
    }
}
