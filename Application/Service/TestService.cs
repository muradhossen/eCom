using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;

namespace Application.Service
{
    public class TestService : Service<Test>, ITestService
    {
        private readonly ITestRepository _repository;

        public TestService(ITestRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
