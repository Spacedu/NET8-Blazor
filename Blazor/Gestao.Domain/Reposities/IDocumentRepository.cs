using Gestao.Domain;

namespace Gestao.Domain.Repositories
{
    public interface IDocumentRepository
    {
        Task Add(Document entity);
        Task Delete(int id);
        Task<Document?> Get(int id);
        Task Update(Document entity);
    }
}