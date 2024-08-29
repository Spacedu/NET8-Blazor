using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Gestao.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public CompanyRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<PaginatedList<Company>> GetAll(Guid applicationUserId, int pageIndex, int pageSize, string searchWord = "")
        {
            using (var _db = _factory.CreateDbContext())
            {
                var items = await _db.Companies
                .Where(a => a.UserId == applicationUserId)
                .Where(a => a.TradeName.Contains(searchWord) || a.LegalName.Contains(searchWord))
                .OrderBy(a => a.TradeName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

                var count = await _db.Companies.Where(a => a.UserId == applicationUserId).Where(a => a.TradeName.Contains(searchWord) || a.LegalName.Contains(searchWord)).CountAsync();

                int totalPages = (int)Math.Ceiling((decimal)count / pageSize);

                return new PaginatedList<Company>(items, pageIndex, totalPages);
            }
        }
        public async Task<Company?> Get(int id)
        {
            using (var _db = _factory.CreateDbContext())
            {
                return await _db.Companies.SingleOrDefaultAsync(a => a.Id == id);
            }
        }
        public async Task Add(Company entity)
        {
            using (var _db = _factory.CreateDbContext())
            {
                _db.Companies.Add(entity);
                await _db.SaveChangesAsync();
            }
        }
        public async Task Update(Company entity)
        {
            using (var _db = _factory.CreateDbContext())
            {
                _db.Companies.Update(entity);
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
                    _db.Companies.Remove(entity);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
