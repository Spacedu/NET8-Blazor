using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Repositories;

namespace Gestao.Client.Services
{
    public class CompanyService : ICompanyRepository
    {
        public Task Add(Company company)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Company?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<Company>> GetAll(Guid applicationUserId, int pageIndex, int pageSize, string searchWord)
        {
            throw new NotImplementedException();
        }

        public Task Update(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
