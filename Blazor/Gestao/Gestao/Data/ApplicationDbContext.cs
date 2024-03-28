using Gestao.Models;
using Gestao.Models.Contas;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<Usuario>(options)
    {
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ContaPagarReceber> ContasPagarReceber { get; set; }
        public DbSet<GrupoContaRecorrente> GruposContasRecorrentes { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }
    }
}
