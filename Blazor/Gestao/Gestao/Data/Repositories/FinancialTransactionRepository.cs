using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Enums;
using Gestao.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data.Repositories
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public FinancialTransactionRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<PaginatedList<FinancialTransaction>> GetAll(int companyId, TypeFinancialTransaction type, int pageIndex, int pageSize, string searchWord = "")
        {
            using (var _db = _factory.CreateDbContext())
            {
                var items = await _db.FinancialTransactions
                .Where(a => a.CompanyId == companyId && a.TypeFinancialTransaction == type)
                .Where(a => a.Description.Contains(searchWord))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

                var count = await _db.FinancialTransactions.Where(a => a.CompanyId == companyId && a.TypeFinancialTransaction == type).Where(a => a.Description.Contains(searchWord)).CountAsync();
                int totalPages = (int)Math.Ceiling((decimal)count / pageSize);

                return new PaginatedList<FinancialTransaction>(items, pageIndex, totalPages);
            }
        }
        public async Task<FinancialTransaction?> Get(int id)
        {
            using (var _db = _factory.CreateDbContext())
            {
                return await _db.FinancialTransactions.OrderByDescending(a => a.ReferenceDate).Include(a => a.Category).Include(a => a.Account).Include(a => a.Documents).SingleOrDefaultAsync(a => a.Id == id);
            }
        }
        public async Task Add(FinancialTransaction entity)
        {
            using (var _db = _factory.CreateDbContext())
            {
                _db.FinancialTransactions.Add(entity);
                await _db.SaveChangesAsync();
            }
        }
        public async Task Update(FinancialTransaction entity)
        {
            using (var _db = _factory.CreateDbContext())
            {
                _db.FinancialTransactions.Update(entity);
                await _db.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            using (var _db = _factory.CreateDbContext())
            {
                var entity = await Get(id);

                if (entity is not null)
                {
                    await Delete(entity);
                }
            }
        }

        public async Task Delete(FinancialTransaction entity)
        {
            using (var _db = _factory.CreateDbContext())
            {
                _db.FinancialTransactions.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<int> GetCountTransactionsSameGroup(int Id)
        {
            using (var _db = _factory.CreateDbContext())
            {
                return await _db.FinancialTransactions.Where(a => a.RepeatGroup == Id).OrderBy(a => a.Id).CountAsync();
            }
        }

        public async Task<List<FinancialTransaction>> GetTransactionsSameGroup(int Id)
        {
            using (var _db = _factory.CreateDbContext())
            {
                return await _db.FinancialTransactions.Where(a => a.RepeatGroup == Id).OrderBy(a => a.Id).ToListAsync();
            }
        }

    }
}
