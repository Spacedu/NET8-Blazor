using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Repositories;

namespace Gestao.Client.Services
{
    public class AccountService : IAccountRepository
    {
        public Task Add(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Account>> GetAll(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<Account>> GetAll(int companyId, int pageIndex, int pageSize, string searchWord)
        {
            throw new NotImplementedException();
        }

        public Task Update(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
