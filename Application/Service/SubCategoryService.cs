using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;

namespace Application.Service;

internal class SubCategoryService : Service<SubCategory>, ISubCategoryService
{
    private readonly ISubCategoryRepository _repository;

    public SubCategoryService(ISubCategoryRepository repository) : base(repository)
    {
        _repository = repository;
    }
}
