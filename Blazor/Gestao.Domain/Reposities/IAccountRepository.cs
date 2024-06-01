using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;

namespace Gestao.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task Add(Account entity);
        Task Delete(int id);
        Task<Account?> Get(int id);
        Task<List<Account>> GetAll(int companyId);
        Task<PaginatedList<Account>> GetAll(int companyId, int pageIndex, int pageSize, string searchWord);
        Task Update(Account entity);
    }
}