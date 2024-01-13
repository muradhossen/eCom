using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;

namespace Application.Service;

internal class ProductService : Service<Product>, IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository) : base(repository)
    {
        _repository = repository;
    }
}
