using Application.RepositoryInterface;
using Application.RepositoryInterface.Base;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;

namespace Application.Service
{
    internal class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
