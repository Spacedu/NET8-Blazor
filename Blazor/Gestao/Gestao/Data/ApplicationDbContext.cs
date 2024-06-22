using Gestao.Data.Interceptors;
using Gestao.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FinancialTransaction>()
                .Property(a => a.Repeat)
                .HasConversion<string>();

            builder.Entity<FinancialTransaction>()
                .Property(a => a.TypeFinancialTransaction)
                .HasConversion<string>();

            builder.Entity<Company>().HasIndex(a => a.TaxId).IsUnique();

            builder.Entity<ApplicationUser>().HasQueryFilter(a => a.DeletedAt == null);
            builder.Entity<Account>().HasQueryFilter(a => a.DeletedAt == null);
            builder.Entity<Company>().HasQueryFilter(a => a.DeletedAt == null);
            builder.Entity<Category>().HasQueryFilter(a => a.DeletedAt == null);
            builder.Entity<FinancialTransaction>().HasQueryFilter(a => a.DeletedAt == null);
            builder.Entity<Document>().HasQueryFilter(a => a.DeletedAt == null);
        }
    }
}
