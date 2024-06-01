using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Repositories;
using System.Net.Http.Json;

namespace Gestao.Client.Services
{
    public class CompanyService : ICompanyRepository
    {
        private readonly HttpClient _httpClient;

        public CompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

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

        public async Task<PaginatedList<Company>> GetAll(Guid applicationUserId, int pageIndex, int pageSize, string searchWord)
        {
            var url = $"/api/companies?applicationUserId={applicationUserId}&pageIndex={pageIndex}&searchWord={searchWord}";
            var entities = await _httpClient.GetFromJsonAsync<PaginatedList<Company>>(url);

            return entities!;
        }

        public Task Update(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
