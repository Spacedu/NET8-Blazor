using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Enums;
using Gestao.Domain.Repositories;
using System.Net.Http.Json;

namespace Gestao.Client.Services
{
    public class FinancialTransactionService : IFinancialTransactionRepository
    {
        private readonly HttpClient _httpClient;

        public FinancialTransactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task Add(FinancialTransaction entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(FinancialTransaction entity)
        {
            throw new NotImplementedException();
        }

        public Task<FinancialTransaction?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedList<FinancialTransaction>> GetAll(int companyId, TypeFinancialTransaction type, int pageIndex, int pageSize, string searchWord)
        {
            var url = $"/api/financialtransactions?companyId={companyId}&pageIndex={pageIndex}&searchWord={searchWord}&type={type}";
            var entities = await _httpClient.GetFromJsonAsync<PaginatedList<FinancialTransaction>>(url);

            return entities!;
        }

        public Task<int> GetCountTransactionsSameGroup(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FinancialTransaction>> GetTransactionsSameGroup(int Id)
        {
            throw new NotImplementedException();
        }

        public Task Update(FinancialTransaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
