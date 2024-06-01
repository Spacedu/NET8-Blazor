using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;

namespace Gestao.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task Add(Company company);
        Task Delete(int id);
        Task<Company?> Get(int id);
        Task<PaginatedList<Company>> GetAll(Guid applicationUserId, int pageIndex, int pageSize, string searchWord);
        Task Update(Company company);
    }
}