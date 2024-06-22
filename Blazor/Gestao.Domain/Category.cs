using Gestao.Domain.Interfaces;

namespace Gestao.Domain
{
    public class Category : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
