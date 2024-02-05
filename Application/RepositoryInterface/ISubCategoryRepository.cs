using Application.RepositoryInterface.Base;
using Domain.Entities;

namespace Application.RepositoryInterface
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task<bool> DeleteSubCategoryHierarchy(long categoryId, long userId);
    }
}
