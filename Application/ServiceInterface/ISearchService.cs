using Application.Common.Pagination;
using Application.DTOs.Search;

namespace Application.ServiceInterface;

public interface ISearchService
{
    Task<PagedList<SearchDto>> GetSearchProducts(SearchPageParams pageParams);
}
