using Application.Common.Pagination;
using Application.DTOs.Search;
using Application.RepositoryInterface;
using Application.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace Application.Service;

public class SearchService : ISearchService
{
    private readonly IProductRepository _productRepository;

    public SearchService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<PagedList<SearchDto>> GetSearchProducts(SearchPageParams pageParams)
    {
        if (string.IsNullOrWhiteSpace(pageParams.SearchKey))
        {
            return PagedList<SearchDto>.Create(new List<SearchDto>(), 0, 0);
        }
        string searchKey = pageParams.SearchKey.ToLower();

        var query = _productRepository.GetSearchesQueryable();

        var search = query
            .Where(c => c.Name.ToLower().Contains(searchKey))
            .Select(c => new SearchDto
            {
                ItemId = c.ItemId,
                ItemName = c.Name,
                ParentId = c.SubCategoryId,
                Type = c.Type,
                Price = c.Price,
                Code = c.Code,
                ImageUrl = c.ImageUrl
            });

        return await PagedList<SearchDto>.CreateAsync(search, pageParams.PageSize, pageParams.PageNumber);


    }
}
