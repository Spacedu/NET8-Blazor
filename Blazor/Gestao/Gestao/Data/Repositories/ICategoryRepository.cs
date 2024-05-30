using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;

namespace Gestao.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task Add(Category entity);
        Task Delete(int id);
        Task<Category?> Get(int id);
        Task<List<Category>> GetAll(int companyId);
        Task<PaginatedList<Category>> GetAll(int companyId, int pageIndex, int pageSize);
        Task Update(Category entity);
    }
}