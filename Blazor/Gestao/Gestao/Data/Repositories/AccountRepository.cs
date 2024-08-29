using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public AccountRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<PaginatedList<Account>> GetAll(int companyId, int pageIndex, int pageSize, string searchWord = "")
        {
            using (var _db = _factory.CreateDbContext())
            {
                var items = await _db.Accounts
                    .Where(a => a.CompanyId == companyId)
                    .Where(a => a.Description.Contains(searchWord))
                    .OrderBy(a => a.Description)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var count = await _db.Accounts.Where(a => a.CompanyId == companyId).Where(a => a.Description.Contains(searchWord)).CountAsync();
                int totalPages = (int)Math.Ceiling((decimal)count / pageSize);

                return new PaginatedList<Account>(items, pageIndex, totalPages);
            }   
        }
        public async Task<List<Account>> GetAll(int companyId)
        {
            using (var _db = _factory.CreateDbContext())
            {
                return await _db.Accounts.Where(a => a.CompanyId == companyId).ToListAsync();
            }
        }
        public async Task<Account?> Get(int id)
        {
            using (var _db = _factory.CreateDbContext())
            {
                return await _db.Accounts.SingleOrDefaultAsync(a => a.Id == id);
            }
        }
        public async Task Add(Account entity)
        {
            using (var _db = _factory.CreateDbContext())
            {
                _db.Accounts.Add(entity);
                await _db.SaveChangesAsync();
            }
        }
        public async Task Update(Account entity)
        {
            using (var _db = _factory.CreateDbContext())
            {
                _db.Accounts.Update(entity);
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
                    _db.Accounts.Remove(entity);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
