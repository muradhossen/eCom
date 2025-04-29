using Application.DTOs.Search;
using Application.ServiceInterface;
using eCom.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Application.Extentions;

namespace eCom.API.Controllers
{
    public class SearchController : BaseApiController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]   
        public async Task<IActionResult> GetSearches([FromQuery] SearchPageParams pageParams)
        {
            var result = await _searchService.GetSearchProducts(pageParams);

            Response.AddPaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPage);

            return Ok(result);
        }
    }
}
