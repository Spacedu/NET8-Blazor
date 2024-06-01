using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Gestao.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<PaginatedList<Company>> GetAll(Guid applicationUserId, int pageIndex, int pageSize, string searchWord = "")
        {
            var items = await _db.Companies
                .Where(a => a.UserId == applicationUserId)
                .Where(a=>a.TradeName.Contains(searchWord) || a.LegalName.Contains(searchWord))
                .OrderBy(a=>a.TradeName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _db.Companies.Where(a => a.UserId == applicationUserId).Where(a => a.TradeName.Contains(searchWord) || a.LegalName.Contains(searchWord)).CountAsync();
            int totalPages = (int)Math.Ceiling((decimal)count / pageSize);

            return new PaginatedList<Company>(items, pageIndex, totalPages);
        }
        public async Task<Company?> Get(int id)
        {
            return await _db.Companies.SingleOrDefaultAsync(a => a.Id == id);
        }
        public async Task Add(Company entity)
        {
            _db.Companies.Add(entity);
            await _db.SaveChangesAsync();
        }
        public async Task Update(Company entity)
        {
            _db.Companies.Update(entity);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(int id)
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
