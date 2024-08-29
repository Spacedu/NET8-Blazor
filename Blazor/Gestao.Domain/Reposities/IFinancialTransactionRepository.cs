using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Enums;

namespace Gestao.Domain.Repositories
{
    public interface IFinancialTransactionRepository
    {
        Task Add(FinancialTransaction entity);
        Task Delete(int id);
        Task Delete(FinancialTransaction entity);
        Task<FinancialTransaction?> Get(int id);
        Task<PaginatedList<FinancialTransaction>> GetAll(int companyId, TypeFinancialTransaction type, int pageIndex, int pageSize, string searchWord);
        Task<int> GetCountTransactionsSameGroup(int Id);
        Task<List<FinancialTransaction>> GetTransactionsSameGroup(int Id);
        Task Update(FinancialTransaction entity);
    }
}