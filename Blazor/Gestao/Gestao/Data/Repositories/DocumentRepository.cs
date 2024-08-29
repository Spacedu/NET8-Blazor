using Gestao.Client.Libraries.Utilities;
using Gestao.Domain;
using Gestao.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public DocumentRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<Document?> Get(int id)
        {
            using (var _db = _factory.CreateDbContext())
            {
                return await _db.Documents.SingleOrDefaultAsync(a => a.Id == id);
            }
        }
        public async Task Add(Document entity)
        {
            using (var _db = _factory.CreateDbContext())
            {
                _db.Documents.Add(entity);
                await _db.SaveChangesAsync();
            }
        }
        public async Task Update(Document entity)
        {
            using (var _db = _factory.CreateDbContext())
            {
                _db.Documents.Update(entity);
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
                    _db.Documents.Remove(entity);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
